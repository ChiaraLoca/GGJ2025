using System.Collections.Generic;
using UnityEngine;

using Utility.GameEventManager;

public class LevelManager : MonoBehaviour
{
    public Transform canvas;
    public StartPanel startPanelPrefab;
    public CantoneLabelsPanel cantoneLabelsPanelPrefab;
    public ResourceLabelsPanel resourceLabelsPanelPrefab;
    public MessagePanel messagePanelPrefab;
    public PopeController popeControllerPrefab;
    
    private void Start()
    {
        Initialzie();
    }

    public void Initialzie()
    {
        StartPanel startPanel = Instantiate(startPanelPrefab, canvas);

    }

    private void Awake()
    {
        EventManager.AddListener<StartEvent>(OnStart);
    }

    private void OnStart(StartEvent evt)
    {
        //Non Qui
        CantoneLabelsPanel cantoneLabelsPanel = Instantiate(cantoneLabelsPanelPrefab, canvas);
        cantoneLabelsPanel.Initialize(new List<string>{"Ticino","Uri","Svito"});

        //ResourceLabelsPanel resourceLabelsPanel = Instantiate(resourceLabelsPanelPrefab, canvas);
        //resourceLabelsPanel.Initialize(new List<string> { "Cavalli", "Rame", "Ferro","Grano","Sale" });

        MessagePanel messagePanel = Instantiate(messagePanelPrefab, canvas);

        PopeController popeController = Instantiate(popeControllerPrefab);
        popeController.Initialize();
    }


    

}