using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private bool _gameRunning = false;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (_gameRunning)
            {
                _currentSeconds += Time.deltaTime;
                TimerUI.instance.UpdateUI(_maxSeconds - _currentSeconds);
                if (_currentSeconds >= _maxSeconds)
                {
                    EndGame();
                }

            }


        }

        public void Start()
        {
            StartGame();
            //DontDestroyOnLoad(this);
        }

        public void Initialize()
        {

            //ScoreManager.Initialize();
            //foreach (interactableBehaviour interactableBehaviour in _interactableehaviours)
            //{
            //    interactableBehaviour.Info.Done = false;
            //}

            //CreatePlayer();
        }



      
        public void StartGame()
        {

            AudioManager.instance.track(2, false);
            _currentSeconds = 0;
            Initialize();
            _gameRunning = true;
        }
        public void EndGame()
        {

           // CaclculateScore();
            _gameRunning = false;
            SceneManager.LoadScene("2_Final_Demo");
        }

        

    }
}
