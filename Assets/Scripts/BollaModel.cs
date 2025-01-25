using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BollaModel
{
    bool check(List<Transaction> transactions);
    string getDescription();
}

public class BanBolla : BollaModel
{
    private List<string> resources;
    private int amount;
    private int mode;

    //mode 1 -> si devono mandare + di amount
    //mode -1 -> si devono mandare - di amount
    //mode 3 -> si devono mandare...

    public BanBolla(int mode, int amount, params string[] resources)
    {
        this.resources = new List<string>(resources);
        this.amount = amount;
        this.mode = mode;
    }
    
    public bool check(List<Transaction> transactions)
    {
        foreach(Transaction t in transactions)
        {
            switch(mode)
            {
                case 1:
                    if(t.amount < amount)
                    {
                        return false;
                    };
                    break;
                case -1:
                    if(t.amount > amount)
                    {
                        return false;
                    }
                    break;
            }
        }
        return true;
    }

    public string getDescription()
    {
        return "Boh";
    }
}
