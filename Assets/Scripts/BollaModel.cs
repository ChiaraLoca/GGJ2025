using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BollaModel
{
    public bool check(List<Transaction> transactions);
    public string getDescription();
    public string getShortDescription();
}

public class BanBolla : BollaModel
{
    private List<string> resources;
    private int amount;
    private int mode;
    private string resourceDescriptionString = " ";

    //mode 1 -> si devono mandare + di amount
    //mode 0 -> si devono mandare - di amount

    public static BanBolla Build(int mode, int amount, params string[] resources)
    {
        return new BanBolla(mode, amount, resources);
    }
    
    public static BanBolla Build()
    {
        int mode = Random.Range(0, 2);
        int amount = Random.Range(10, 31);
        int resourceN = Random.Range(1, GameModel.Risorse.Count+1);

        List<int> indexes = new List<int>();
        for (int i = 0; i < resourceN; i++)
        {
            int newIndex = Random.Range(0, GameModel.Risorse.Count);
            if (!indexes.Contains(newIndex))
            {
                indexes.Add(newIndex);
            }
        }
        string[] arguments = new string[indexes.Count];
        for (int i = 0; i < indexes.Count; i++)
        {
            arguments[i] = GameModel.Risorse[indexes[i]].Type;
        }
        return new BanBolla(mode, amount, arguments);
    }

    public BanBolla(int mode, int amount, params string[] resources)
    {
        this.resources = new List<string>(resources);
        this.amount = amount;
        this.mode = mode;
        for(int i=0; i<resources.Length; i++)
        {
            resourceDescriptionString += GameModel.DizionarioRisorse[resources[i]];
            if (resources.Length>1 && i!= resources.Length-1)
            {
                resourceDescriptionString += ",";
            }
            resourceDescriptionString += " ";
        }
    }
    
    public bool check(List<Transaction> transactions)
    {
        Dictionary<string, int> resourcesSent = new Dictionary<string, int>(); 
        foreach(Transaction t in transactions)
        {
            if(resourcesSent.ContainsKey(t.resource))
            {
                resourcesSent[t.resource] += t.amount;
            } 
            else
            {
                resourcesSent.Add(t.resource, t.amount);
            }
        }
        foreach(var res in resourcesSent)
        {
            if (resources.Contains(res.Key)){
                switch (mode)
                {
                    case 0:
                        if (res.Value > amount)
                        {
                            return false;
                        };
                        break;
                    case 1:
                        if (res.Value < amount)
                        {
                            return false;
                        }
                        break;
                }
            }
           
        }
        return true;
    }

    public string getDescription()
    {
        return "Bolla Papale di Sua Santità Sisto IV<br>" +
            "Anno del Signore 1476<br>" +
            "Noi, Sisto IV, per l'autorità che ci è conferita dalla Santa Sede, decretiamo con il nostro pieno potere che, a partire dalla data di pubblicazione della presente, nessun commerciante debba inviare un numero<b> " + (mode == 0 ? "superiore" : "inferiore") + " a " + amount + " " + resourceDescriptionString + " </b> per missiva.<br>" +
            "Questo decreto è valido in tutte le terre sotto la nostra giurisdizione e <u>rimarrà in vigore </u> fino a nuova disposizione.<br>";
    }

    public string getShortDescription()
    {
        return "" + (mode == 0 ? "Massimo" : "Minimo") + " di " + amount + " " + resourceDescriptionString + " totali per mail";
    }
}

public class ResourceNumberBolla : BollaModel
{
    private int amount;
    private int mode;

    //mode 1 -> si devono mandare + di amount
    //mode 0 -> si devono mandare - di amount


    public static ResourceNumberBolla Build(int mode, int amount)
    {
        return new ResourceNumberBolla(mode, amount);
    }

    public static ResourceNumberBolla Build()
    {
        int mode = Random.Range(0, 2);
        int amount = Random.Range(2, GameModel.Risorse.Count);
        return new ResourceNumberBolla(mode, amount);
    }

    public ResourceNumberBolla(int mode, int amount)
    {
        this.amount = amount;
        this.mode = mode;
    }

    public bool check(List<Transaction> transactions)
    {
        ISet<string> uniqueResources = new HashSet<string>();
        foreach (Transaction t in transactions)
        {

            uniqueResources.Add(t.resource);

        }
        switch (mode)
        {
            case 0:
                if (uniqueResources.Count < amount)
                {
                    return false;
                };
                break;
            case 1:
                if (uniqueResources.Count > amount)
                {
                    return false;
                }
                break;
        }
        return true;
    }

    public string getDescription()
    {
        return
"Bolla Papale di Sua Santità Sisto IV<br>" +
"Anno del Signore 1476<br>" +
"Noi, Sisto IV, per l'autorità che ci è conferita dalla Santa Sede, " +
"decretiamo con il nostro potere divino che, a partire dalla data di pubblicazione della presente, ogni missiva commerciale dovrà trattare<b> " + (mode == 0 ? " almeno " : " al massimo ") + amount + " tipi di risorse.<br>" +
"Questo decreto si applica in tutte le terre sotto la nostra giurisdizione e <u>rimarrà in vigore</u> fino a nuova disposizione.<br>";
    }

    public string getShortDescription()
    {
        return "" + (mode == 0 ? "Minimo" : "Massimo") + " " + amount + " risorse diverse per mail";
    }
}

//almeno x risorse assieme
