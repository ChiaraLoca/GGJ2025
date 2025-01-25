using System;
using System.Collections.Generic;
using UnityEngine;

using Utility.GameEventManager;


public class LevelManager : MonoBehaviour
{
    public Transform canvas;
    public StartPanel startPanelPrefab;
    public AddCantoneLabelsPanel addCantoneLabelsPanelPrefab;
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

        AddCantoneLabelsPanel addCantoneLabelsPanel = Instantiate(addCantoneLabelsPanelPrefab, canvas);


        MessagePanel messagePanel = Instantiate(messagePanelPrefab, canvas);

        PopeController popeController = Instantiate(popeControllerPrefab);
        popeController.Initialize();

        InvokeRepeating("CheckForNewMails", 1, 8);

        StartPanel startPanel = Instantiate(startPanelPrefab, canvas);
    }

    private void Awake()
    {
        EventManager.AddListener<StartEvent>(OnStart);
    }

    private void OnStart(StartEvent evt)
    {
        EventManager.Broadcast(new HideMessageEvent());
       
        CantoneLabelsPanel cantoneLabelsPanel = Instantiate(cantoneLabelsPanelPrefab, canvas);
        cantoneLabelsPanel.Initialize();

        ResourceLabelsPanel resourceLabelsPanel = Instantiate(resourceLabelsPanelPrefab, canvas);
        resourceLabelsPanel.Initialize(new List<string> { "Cavalli", "Rame", "Ferro","Grano","Sale" });

        CancelInvoke("CheckForNewMails");
    }


    public void CheckForNewMails()
    {
        List<MailModel> mails = MailController.RetrieveEmail();
        Debug.Log("CheckForNewMails "+string.Join(", ",mails));

        foreach (MailModel mail in mails)
        {
            GameStatus.GameStatusManager.instance.AddPlayer(mail);
        }
    }


}