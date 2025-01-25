using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Parser
{
    public static Parser Instance { get; private set; }
    public Parser GetInstance()
    {
        if(Instance == null)
        {
            Instance = new Parser();
        }
        return Instance;
    }

    public int Parse(string text)
    {
        var transactions = Regex.Split(text, "\r\n|\r|\n");
        int errors = 0;
        foreach(string line in transactions)
        {
            errors += ParseLine(line);
        }
        return errors;
    }

    private int ParseLine(string line)
    {
        var split = Regex.Split(line, " ");
        string target = split[0];
        PlayerModel player = GameModel.FindPlayer(target);
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
        if(!player.Score.checkResourceName(resource))
        {
            return 1;
        }
        if (player.Score.enoughResource(resource, realAmount))
        {
            player.Score.addResource(resource, realAmount);
        } else
        {
            return 1;
        }
        
        return 0;
    }
}
