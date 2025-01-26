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


    public static int Parse(string rawText, PlayerModel mittente)
    {
        string text = cleanInput(rawText);
        var transactionText = Regex.Split(text, "\r\n|\r|\n");
        List<Transaction> transactions = new List<Transaction>();
        int righeEseguite = 0;
        foreach(string line in transactionText)
        {
            righeEseguite += ParseLine(line, transactions, mittente);
        }
        if (!GameModel.checkBolle(transactions))
        {
            EventManager.Broadcast(new ScomunicaEvent(mittente));
        }
        return righeEseguite;
    }

    private static string cleanInput(string rawText)
    {
        return rawText.Split("<")[0];
    }

    private static int ParseLine(string line, List<Transaction> transactions, PlayerModel mittente)
    {
        var split = Regex.Split(line, " ");
        string target = split[0];
        PlayerModel player = GameStatusManager.instance.FindPlayer(target);
        if(player == null)
        {
            return 0;
        }
        int realAmount;
        bool amountReal = int.TryParse(split[1], out realAmount) && realAmount>=0;
        if(!amountReal)
        {
            return 0;
        }

        string resource = split[2];
        string realResource = player.Score.checkResourceName(resource.ToLower());


        if(realResource == null)
        {
            return 0;
        }

        if (player.Score.enoughResource(realResource, realAmount))
        {
            player.Score.addResource(realResource, realAmount);
            mittente.Score.removeResource(realResource, realAmount);
        } 
        else
        {
            return 0;
        }
        transactions.Add(new Transaction(player, realAmount, realResource));
        return 1;
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
