using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.GameEventManager;

namespace GameStatus
{

    public class GameStatusManager : MonoBehaviour
    {
        public static GameStatusManager instance;
     
        [SerializeField] private float _maxSeconds;
        [SerializeField] private float _currentSeconds;
        public TimerPanel _timerPanelPrefab;
        public TimerPanel _timerPanel;
        public Transform canvasParent;
        public List<PlayerModel> Players = new List<PlayerModel>();
        private bool _gameRunning = false;
        private bool _timerRun = false;

       

        public PlayerModel FindPlayer(string name)
        {
            return Players.Find(player => player.Name == name);
        }
        public PlayerModel FindPlayerByMail(string mail)
        {
            return Players.Find(player => player.Mail == mail);
        }

        public void AddPlayer(MailModel mailModel)
        {
            PlayerModel found = FindPlayerByMail(mailModel.MailFrom);
            if (found == null)
            {
                PlayerModel newPlayer = new PlayerModel();
                newPlayer.Mail = mailModel.MailFrom;
                newPlayer.Name = GetRandomName();
                newPlayer.Quest = QuestController.GetRandomQuest();
                newPlayer.Score = new Score();
                Players.Add(newPlayer);
                EventManager.Broadcast(new AddNewCantoneEvent(newPlayer));
                MailController.SendEmailAsync(newPlayer.Mail,"Epistola", MessageHelper.GetMailTextGameStart(newPlayer.Name,newPlayer.Quest.Description));
            }

        }

        public string GetRandomName()
        { 
            int randomIndex = UnityEngine.Random.Range(0, GameModel.CantoniSvizzeri.Count);
            String res = GameModel.CantoniSvizzeri[randomIndex];
            GameModel.CantoniSvizzeri.RemoveAt(randomIndex);
            return res;

        }

        private void Awake()
        {
            instance = this;

            EventManager.AddListener<StartEvent>(StartTimer);
        }

        

        private void StartTimer(StartEvent evt)
        {
            
            _timerRun=true;

            _timerPanel = Instantiate(_timerPanelPrefab, canvasParent);
            
        }

        private void Update()
        {
            if (_timerRun)
            {
                _currentSeconds += Time.deltaTime;
                _timerPanel.SetTimer(Mathf.RoundToInt(_maxSeconds - _currentSeconds));
                if (_currentSeconds >= _maxSeconds)
                {
                    EndGame();
                }

            }


        }


        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {

          //  AudioManager.instance.track(2, false); TODO AUDIOMANAGER
            _currentSeconds = 0;
            //TODO LEVEL START 
            _gameRunning = true;
        }
        public void EndGame()
        {

            CaclculateScore();
            _gameRunning = false;
          //  SceneManager.LoadScene("2_Final_Demo");
        }

        private static void CaclculateScore()
        {
            foreach (PlayerModel player in instance.Players)
            {
                if (QuestController.Check(player, instance.Players))
                {
                    MailController.SendEmailAsync(player.Mail, "La sua gratitudine è stata Ricompensata", MessageHelper.GetMailTextVittoria(player.Name));
                    continue;
                }
                MailController.SendEmailAsync(player.Mail, "Dilectis in Christo filiis, salutem et apostolicam benedictionem", MessageHelper.GetMailTextSconfitta(player.Name));
     

            }
        }

        public void Scomunica(PlayerModel player)
        {
            player.Scomunica = true;
        }

    }
}
