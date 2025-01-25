using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.GameEventManager;

namespace GameStatus
{
    //public class ScoreManager
    //{
    //    public static int score;
    //    public static int death;
    //    public static int miss;

    //    internal static void Initialize()
    //    {
    //        score = 0; death = 0; miss = 0;
    //    }
    //}

    public class GameStatusManager : MonoBehaviour
    {
        public static GameStatusManager instance;
     
        [SerializeField] private float _maxSeconds;
        [SerializeField] private float _currentSeconds;
        public List<PlayerModel> Players = new List<PlayerModel>();
        private bool _gameRunning = false;

       

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
                Players.Add(newPlayer);
                EventManager.Broadcast(new AddNewCantoneEvent(newPlayer));
                MailController.SendEmail(newPlayer.Mail,"Epistola", MessageHelper.GetMailTextGameStart(newPlayer.Name,newPlayer.Quest.Description));
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
        }

        private void Update()
        {
            if (_gameRunning)
            {
               /* _currentSeconds += Time.deltaTime;
                TimerUI.instance.UpdateUI(_maxSeconds - _currentSeconds);
                if (_currentSeconds >= _maxSeconds)
                {
                    EndGame();
                }*/

            }


        }

        public void Start()
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
                    MailController.SendEmail(player.Mail, "La sua gratitudine è stata Ricompensata", MessageHelper.GetMailTextVittoria(player.Name));
                    continue;
                }
                MailController.SendEmail(player.Mail, "Dilectis in Christo filiis, salutem et apostolicam benedictionem", MessageHelper.GetMailTextSconfitta(player.Name));
     

            }
        }

        public void Scomunica(PlayerModel player)
        {
            player.Scomunica = true;
        }

    }
}
