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

    private Dictionary<string, List<string>> resourceNameMap = new Dictionary<string, List<string>>() {
        ["horse"] = new List<string>() { "cavallo", "cavalli", "horse", "horses" },
        ["copper"] = new List<string>() { "rame", "copper"},
        ["iron"] = new List<string>() { "ferro", "iron"},
        ["salt"] = new List<string>() { "sale", "salt"},
        ["wheat"] = new List<string>() { "wheat", "grano"}
    };

    private Dictionary<string, int> resourceMap = new Dictionary<string, int>()
    {
        ["horse"] = 100,
        ["copper"] = 100,
        ["iron"] = 100,
        ["salt"] = 100,
        ["wheat"] = 100
    };

    public string checkResourceName(string name)
    {
        foreach(KeyValuePair<string, List<string>> entry in resourceNameMap) {
            if (entry.Value.Contains(name))
            {
                return entry.Key;
            }
        };
        return null;
    }

    public void addResource(string resource, int amount)
    {
        resourceMap[resource] += amount;
    }

    public void removeResource(string resource, int amount)
    {
        resourceMap[resource] -= amount;
    }

    public bool enoughResource(string resource, int amount)
    {
        return resourceMap[resource] >= amount;
    }

    public int Horses { get; set; }
    public int Coppers { get; set; }
    public int Irons { get; set; }
    public int Salt { get; set; }
    public int Wheat { get; set; }
}



public class MailModel
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string MailTo { get; set; }
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
        "Uri", "Svitto", "Untervaldo", "Glarona", "Zugo", "Lucerna", "Zurigo", "Basilea", "Sciaffusa", "Appenzello", "Grisone", "Berna", "Soletta", "Friburgo", "Losanna", "Ginevra", "Neuch�tel", "Vaud", "Valais", "Ticino"
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




