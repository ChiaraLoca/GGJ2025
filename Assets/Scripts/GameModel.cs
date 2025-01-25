using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public Score Score { get; set; }
    public bool Scomunica { get; set; }
    public Quest Quest { get; set; }



}

public class Quest
{
    public string Description { get; set; }
    public Func<Score, List<PlayerModel>, bool> Check { get; set; }
}

public class Score
{
    public Score()
    {
        Resources = new Dictionary<string, int>()
        {
            ["Horses"] = 100,
            ["Coppers"] = 100,
            ["Irons"] = 100,
            ["Salt"] = 100,
            ["Wheat"] = 100
        };
        ResourceNameMap = new Dictionary<string, List<string>>()
        {
            ["Horses"] = new List<string>() { "cavallo", "cavalli", "horse", "horses" },
            ["Coppers"] = new List<string>() { "rame", "copper" },
            ["Irons"] = new List<string>() { "ferro", "iron" },
            ["Salt"] = new List<string>() { "sale", "salt" },
            ["Wheat"] = new List<string>() { "wheat", "grano" }
        };

    }

    private Dictionary<string, int> Resources { get; set; }
    private Dictionary<string, List<string>> ResourceNameMap { get; set; }
    public string checkResourceName(string name)
    {

        
        foreach(var entry in ResourceNameMap) {
            if (entry.Value.Contains(name.ToLower()))
            {
                return entry.Key;
            }
        };
        return null;
    }

    public void addResource(string resource, int amount)
    {
        Resources[resource] += amount;
    }

    public void removeResource(string resource, int amount)
    {
        Resources[resource] -= amount;
    }

    public bool enoughResource(string resource, int amount)
    {
        return Resources[resource] >= amount;
    }

    public int getResource(string resource)
    {
        return Resources[resource];
    }

   

    
}



public class MailModel
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string MailFrom { get; set; }

    public override string ToString()
    {
        return base.ToString();
    }
}


public static class GameModel
{
    public static List<PlayerModel> Players { get; set; }

    public static PlayerModel FindPlayer(string name)
    {
        return Players.Find(player => player.Name == name);
    }

    public static List<BollaModel> bolle = new List<BollaModel>();

    public static bool checkBolle(List<Transaction> transactions)
    {
        foreach(BollaModel bolla in bolle)
        {
            if(!bolla.check(transactions))
            {
                return false;
            }
        }
        return true;
    }

    public static List<string> CantoniSvizzeri = new List<string>
    {
        "Uri", "Svitto", "Untervaldo", "Glarona", "Zugo", "Lucerna", "Zurigo", "Basilea", "Sciaffusa", "Appenzello", "Grisone", "Berna", "Soletta", "Friburgo", "Losanna", "Ginevra", "Vaud", "Valais", "Ticino"
    };

    public static List<(string Description, string Type)> Risorse = new List<(string, string)>
    {
        ("Cavalli", "Horses"),
        ("Rame", "Coppers"),
        ("Ferro", "Irons"),
        ("Sale", "Salt"),
        ("Grano", "Wheat")
    };
}




