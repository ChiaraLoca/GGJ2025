using GameStatus;
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

    [SerializeField] int secondsRefresh = 4;


    private void Start()
    {
        Initialzie();
    }

    public void Initialzie()
    {

        AddCantoneLabelsPanel addCantoneLabelsPanel = Instantiate(addCantoneLabelsPanelPrefab, canvas);

       

        

        InvokeRepeating("CheckForNewMails", 1, secondsRefresh);

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

        MessagePanel messagePanel = Instantiate(messagePanelPrefab, canvas);

        PopeController popeController = Instantiate(popeControllerPrefab);
        popeController.Initialize();

        CancelInvoke("CheckForNewMails");
        InvokeRepeating("CheckForGameEmail", 1, secondsRefresh);
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

    public void CheckForGameEmail()
    {
        List<MailModel> mails = MailController.RetrieveEmail();
        Debug.Log("CheckForNewMails " + string.Join(", ", mails));

        foreach (MailModel mail in mails)
        {
            PlayerModel p = GameStatusManager.instance.FindPlayerByMail(mail.MailFrom);
            int errors = Parser.Parse(mail.Body,p );
            MailController.SendEmail(mail.MailFrom, "Epistola", MessageHelper.GetMailTextEstrattoConto(p, errors));
        }
    }


}