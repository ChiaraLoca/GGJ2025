using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BollaModel
{
    bool check();
    string getDescription();
}

public class BanBolla : BollaModel
{
    BanBolla(int mode, int amount, params string[] resources)
    {
        this.resources = new List<string>(resources);
        this.amount = amount;
    }
    private List<string> resources;
    private int amount;
    public bool check()
    {
        return true;
    }

    public string getDescription()
    {
        throw new System.NotImplementedException();
    }
}
