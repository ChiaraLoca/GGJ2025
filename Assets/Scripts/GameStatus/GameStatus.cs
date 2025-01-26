using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        private TimerPanel _timerPanel;
        public Transform canvasParent;
        public List<PlayerModel> Players = new List<PlayerModel>();
        private bool _gameRunning = false;
        private bool _timerRun = false;
        public static string _gameUID;


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
                newPlayer.Quest = QuestController.GetEmptyQuest();
                newPlayer.Score = new Score();
                Players.Add(newPlayer);
                EventManager.Broadcast(new AddNewCantoneEvent(newPlayer));
                MailController.SendEmailAsync(newPlayer.Mail,"Epistola " + _gameUID, MessageHelper.GetMailTextGameStart(newPlayer.Name,newPlayer.Quest.Description));
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
            _gameUID = ConvertDateToRoman( DateTime.Now);
        }
        public void EndGame()
        {
            _timerRun = false;
            CaclculateScore();
            _gameRunning = false;
          //  SceneManager.LoadScene("2_Final_Demo");
        }

        private static void CaclculateScore()
        {
            List<(PlayerModel player, bool good)> score = new List<(PlayerModel, bool)>();

            foreach (PlayerModel player in instance.Players)
            {
                if (QuestController.Check(player, instance.Players))
                {
                    MailController.SendEmailAsync(player.Mail, "La sua gratitudine è stata Ricompensata", MessageHelper.GetMailTextVittoria(player.Name));

                    score.Add((player, true));

                    continue;
                }

                score.Add((player, false));

                MailController.SendEmailAsync(player.Mail, "Dilectis in Christo filiis, salutem et apostolicam benedictionem", MessageHelper.GetMailTextSconfitta(player.Name));
            

            }

            EventManager.Broadcast(new EndGameEvent(score));
        }
            
        public void Scomunica(PlayerModel player)
        {
            player.Scomunica = true;
        }

        private static readonly string[] RomanThousands = { "", "M", "MM", "MMM" };
        private static readonly string[] RomanHundreds = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        private static readonly string[] RomanTens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        private static readonly string[] RomanOnes = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        public static string ConvertDateToRoman(DateTime date)
        {
            StringBuilder romanDate = new StringBuilder();

            romanDate.Append(ConvertToRoman(date.Day));
            romanDate.Append("·");
            romanDate.Append(ConvertToRoman(date.Month));
            romanDate.Append("·");
            romanDate.Append(ConvertToRoman(date.Year));
            romanDate.Append(" HORAE ");
            romanDate.Append(ConvertToRoman(date.Hour));
            romanDate.Append(":");
            romanDate.Append(ConvertToRoman(date.Minute));

            return romanDate.ToString();
        }

        private static string ConvertToRoman(int number)
        {
            return RomanThousands[number / 1000] +
                   RomanHundreds[(number % 1000) / 100] +
                   RomanTens[(number % 100) / 10] +
                   RomanOnes[number % 10];
        }

    }

    public class EndGameEvent : IGameEvent
    {
        public List<(PlayerModel player, bool good)> score;

        public EndGameEvent(List<(PlayerModel player, bool good)> score)
        {
            this.score = score;
        }
    }

    
}
