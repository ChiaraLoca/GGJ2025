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
    BanBolla(params string[] resources)
    {
        this.resources = new List<string>(resources);
    }
    private List<string> resources;
    public bool check()
    {
        throw new System.NotImplementedException();
    }

    public string getDescription()
    {
        throw new System.NotImplementedException();
    }
}
