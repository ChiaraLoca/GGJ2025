using GameStatus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utility.GameEventManager;

public class ScomunicaEvent : IGameEvent
{
   public PlayerModel player;

    public ScomunicaEvent(PlayerModel player)
    {
        this.player = player;
    }
}

public static class Parser
{


    public static int Parse(string text, PlayerModel mittente)
    {
        var transactionText = Regex.Split(text, "\r\n|\r|\n");
        List<Transaction> transactions = new List<Transaction>();
        int errors = 0;
        foreach(string line in transactionText)
        {
            errors += ParseLine(line, transactions, mittente);
        }
        if (!GameModel.checkBolle(transactions))
        {
            EventManager.Broadcast(new ScomunicaEvent(mittente));
        }
        return errors;
    }

    private static int ParseLine(string line, List<Transaction> transactions, PlayerModel mittente)
    {
        var split = Regex.Split(line, " ");
        string target = split[0];
        PlayerModel player = GameStatusManager.instance.FindPlayer(target);
        if(player == null)
        {
            return 1;
        }
        int realAmount;
        bool amountReal = int.TryParse(split[1], out realAmount) && realAmount>=0;
        if(!amountReal)
        {
            return 1;
        }

        string resource = split[2];
        string realResource = player.Score.checkResourceName(resource.ToLower());


        if(realResource == null)
        {
            return 1;
        }

        if (player.Score.enoughResource(realResource, realAmount))
        {
            player.Score.addResource(realResource, realAmount);
            mittente.Score.removeResource(realResource, realAmount);
        } 
        else
        {
            return 1;
        }
        transactions.Add(new Transaction(player, realAmount, realResource));
        return 0;
    }
}

public class Transaction
{

    public PlayerModel target { get; set; }
    public int amount { get; set; }
    public string resource { get; set; }

    public Transaction(PlayerModel target, int amount, string resource)
    {
        this.resource = resource;
        this.amount = amount;
        this.target = target;
    }
}
