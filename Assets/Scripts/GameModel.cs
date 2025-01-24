using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public Score Score { get; set; }
    public int IdObjective { get; set; }
}

public class Score
{
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

public class GameModel
{
    private static GameModel Instance;

    public GameModel GetInstance()
    {
        if (Instance == null)
        {
            Instance = new GameModel();
        }
        return Instance;
    }

    public List<PlayerModel> Players { get; set; }
}

public List<string> CantoniSvizzeri = new List<string>
{
    "Uri", "Svitto", "Untervaldo", "Glarona", "Zugo", "Lucerna", "Zurigo", "Basilea", "Sciaffusa", "Appenzello", "Grisone", "Berna", "Soletta", "Friburgo", "Losanna", "Ginevra", "Neuchâtel", "Vaud", "Valais", "Ticino"
};



