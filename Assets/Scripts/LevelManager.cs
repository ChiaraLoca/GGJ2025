using GameStatus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public BollaBoardPanel bollaBoardPanelPrefab;

    [SerializeField] int secondsRefresh = 1;


    private void Start()
    {
        Initialzie();
    }

    public void Initialzie()
    {
        GameModel.Init();
      //  AudioManager.instance.Track(1, true);
        //AddCantoneLabelsPanel addCantoneLabelsPanel = Instantiate(addCantoneLabelsPanelPrefab, canvas);

        CantoneLabelsPanel cantoneLabelsPanel = Instantiate(cantoneLabelsPanelPrefab, canvas);

        StartCoroutine(CheckForNewMails());
        

        StartPanel startPanel = Instantiate(startPanelPrefab, canvas);

       
    }

    

    private void Awake()
    {
        EventManager.AddListener<StartEvent>(OnStart);
        EventManager.AddListener<EndGameEvent>(OnEnd);
    }

    private void OnEnd(EndGameEvent evt)
    {
        StopAllCoroutines();
    }

    private void OnStart(StartEvent evt)
    {
        
       
       BollaBoardPanel bollaBoardPanel = Instantiate (bollaBoardPanelPrefab, canvas);

        

        MessagePanel messagePanel = Instantiate(messagePanelPrefab, canvas);

        PopeController popeController = Instantiate(popeControllerPrefab);
        popeController.Initialize();

        StopAllCoroutines();
        CancelInvoke("CheckForNewMails");

        SetPlayerResources();
        QuestController.InitializeQuestList();

        ResourceLabelsPanel resourceLabelsPanel = Instantiate(resourceLabelsPanelPrefab, canvas);
        resourceLabelsPanel.Initialize(GameModel.Risorse.Select(obj=>obj.Description).ToList());
        

        SendPlayerQuests();
        InvokeRepeating("CheckForGameEmail", 1, secondsRefresh);
        StartCoroutine(CheckForGameEmail());
    }

    private void SendPlayerQuests()
    {
        foreach (var player in GameStatusManager.instance.Players)
        {
            player.Quest = QuestController.GetRandomQuest();
            MailController.SendEmailAsync(player.Mail, "Epistola " + GameStatus.GameStatusManager._gameUID, MessageHelper.GetMailTextGameStart(player.Name, player.Quest.Description));
        }
    }

    public void SetPlayerResources()
    { var players = GameStatusManager.instance.Players.Count;
        if (players <= 3)
        {
            GameModel.Risorse = GameModel.Risorse.GetRange(0, 2);
            return;
        }
        if(players <= 5)
        {
            GameModel.Risorse = GameModel.Risorse.GetRange(0,3);
            return;
        }
        if (players <= 7)
        {
            GameModel.Risorse = GameModel.Risorse.GetRange(0, 4);
            return;
        }
        return;
        
    }


    public IEnumerator CheckForNewMails()
    {
        while (true)
        {
           
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
            
            List<MailModel> mails = MailController.GetRetrievedEmailList();
            Debug.Log("CheckForGameEmail " + string.Join(", ", mails));

            foreach (MailModel mail in mails)
            {
                PlayerModel p = GameStatusManager.instance.FindPlayerByMail(mail.MailFrom);
                if (p.Scomunica)
                    continue;
                int righeEseguite = Parser.Parse(mail.Body, p);
                MailController.SendEmailAsync(mail.MailFrom, "Epistola " + GameStatus.GameStatusManager._gameUID, MessageHelper.GetMailTextEstrattoConto(p, righeEseguite));

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