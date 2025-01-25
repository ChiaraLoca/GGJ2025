using System.Collections.Generic;
using UnityEngine;

public class ResourceLabelsPanel : MonoBehaviour
{
    public ResourceLabel resourceLabelPrefab;
    public Transform labelsParent;
    
    

    public void Initialize(List<string> strings)
    {
        foreach (string s in strings)
        {
            ResourceLabel label = Instantiate(resourceLabelPrefab, labelsParent);
            label.SetLabel(s);
        }
    }
}
