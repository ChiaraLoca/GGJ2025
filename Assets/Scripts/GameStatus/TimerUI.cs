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

        public void UpdateUI(float val)
        {
            time.text = "" + (int)(val);
        }


    }
}
