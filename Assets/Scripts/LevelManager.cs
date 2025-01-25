using GameStatus;
using System;
using System.Collections;
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
        GameModel.Init();

        AddCantoneLabelsPanel addCantoneLabelsPanel = Instantiate(addCantoneLabelsPanelPrefab, canvas);




        StartCoroutine(CheckForNewMails());
        //InvokeRepeating("CheckForNewMails", 1, secondsRefresh);

        StartPanel startPanel = Instantiate(startPanelPrefab, canvas);

       
    }

    private void Awake()
    {
        EventManager.AddListener<StartEvent>(OnStart);
    }

    private void OnStart(StartEvent evt)
    {
        
       
        CantoneLabelsPanel cantoneLabelsPanel = Instantiate(cantoneLabelsPanelPrefab, canvas);
        cantoneLabelsPanel.Initialize();

        ResourceLabelsPanel resourceLabelsPanel = Instantiate(resourceLabelsPanelPrefab, canvas);
        resourceLabelsPanel.Initialize(new List<string> { "Cavalli", "Rame", "Ferro","Grano","Sale" });

        MessagePanel messagePanel = Instantiate(messagePanelPrefab, canvas);

        PopeController popeController = Instantiate(popeControllerPrefab);
        popeController.Initialize();

        StopAllCoroutines();
        //CancelInvoke("CheckForNewMails");
        //InvokeRepeating("CheckForGameEmail", 1, secondsRefresh);
        StartCoroutine(CheckForGameEmail());
    }


    public IEnumerator CheckForNewMails()
    {
        while (true)
        {
            yield return StartCoroutine(MailController.RunRetrieveEmailAsyncAsCoroutine());
            List<MailModel> mails = MailController.GetRetrievedEmailList();
            Debug.Log("CheckForNewMails " + string.Join(", ", mails));

            foreach (MailModel mail in mails)
            {
                GameStatus.GameStatusManager.instance.AddPlayer(mail);
            }
            yield return new WaitForSeconds(secondsRefresh);
        }
    }

    public IEnumerator CheckForGameEmail()
    {
        while (true)
        {
            yield return StartCoroutine(MailController.RunRetrieveEmailAsyncAsCoroutine());
            List<MailModel> mails = MailController.GetRetrievedEmailList();
            Debug.Log("CheckForGameEmail " + string.Join(", ", mails));

            foreach (MailModel mail in mails)
            {
                PlayerModel p = GameStatusManager.instance.FindPlayerByMail(mail.MailFrom);
                if (p.Scomunica)
                    continue;
                int errors = Parser.Parse(mail.Body, p);
                MailController.SendEmail(mail.MailFrom, "Epistola", MessageHelper.GetMailTextEstrattoConto(p, errors));

            }

            if (mails != null && mails.Count > 0)
            {
                EventManager.Broadcast(new SendResponseEvent(mails));
            }
            yield return new WaitForSeconds(secondsRefresh);
        }
    }


}

public class SendResponseEvent : IGameEvent
{
    public List<MailModel> mails;

    public SendResponseEvent(List<MailModel> mails)
    {
        this.mails = mails;
    }
}