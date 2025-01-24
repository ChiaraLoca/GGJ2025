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
        
        int amount = int.Parse(split[1]);
        string resource = split[2];
        return 0;
    }

    private void ApplyTransaction(string target, int amount, string resource)
    {

    }
}
