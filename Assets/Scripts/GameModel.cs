using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public Score Score { get; set; }

    public int IdObjective { get; set; }

    public static bool ResourcesAvailable(string resource, int amount)
    {

    }
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

    public bool checkResourceName(string name)
    {
        foreach(KeyValuePair<string, List<string>> entry in resourceNameMap) {
            if (entry.Value.Contains(name))
            {
                return true;
            }
        };
        return false;
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


public class Objective
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int? MaxHorses { get; set; }
    public int? MinHorses { get; set; }
    public int? MaxCoppers { get; set; }
    public int? MinCoppers { get; set; }
    public int? MaxIrons { get; set; }
    public int? MinIrons { get; set; }
    public int? MaxSalt { get; set; }
    public int? MinSalt { get; set; }
    public int? MaxWheat { get; set; }
    public int? MinWheat { get; set; }

}

public static class GameModel
{
    public static List<PlayerModel> Players { get; set; }

    public static PlayerModel FindPlayer(string name)
    {
        return Players.Find(player => player.Name == name);
    }

    public static List<string> CantoniSvizzeri = new List<string>
{
    "Uri", "Svitto", "Untervaldo", "Glarona", "Zugo", "Lucerna", "Zurigo", "Basilea", "Sciaffusa", "Appenzello", "Grisone", "Berna", "Soletta", "Friburgo", "Losanna", "Ginevra", "Neuchâtel", "Vaud", "Valais", "Ticino"
};
}




