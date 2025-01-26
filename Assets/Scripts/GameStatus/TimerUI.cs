using Org.BouncyCastle.Asn1.Cms;
using TMPro;
using UnityEngine;

namespace GameStatus
{
    public class TimerUI : MonoBehaviour
    {
        public static TimerUI instance;
        public void Awake()
        {
            instance = this;
        }
        [SerializeField] TextMeshProUGUI time;

        public void UpdateUI(int totalSeconds)
        {
            time.gameObject.SetActive(true);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            time.text = formattedTime;
        }


    }
}
