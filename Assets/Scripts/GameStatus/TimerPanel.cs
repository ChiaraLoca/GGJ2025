using TMPro;
using UnityEngine;
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

    public class TimerPanel : MonoBehaviour
    {
        public TextMeshProUGUI time;

        public void SetTimer(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            time.text = formattedTime;
        }

        private void Awake()
        {
           
            EventManager.AddListener<EndGameEvent>(OnEnd);
        }

        private void OnEnd(EndGameEvent @event)
        {
            Destroy(this.gameObject);
        }
    }
}
