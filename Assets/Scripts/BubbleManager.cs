using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility.GameEventManager;


    public class BubbleManager : MonoBehaviour
    {
        public List<float> bubbleTimes;

        private void Start()
        {
            foreach (var bubbleTime in bubbleTimes)
            {
                StartCoroutine(WaitForBubble(bubbleTime));
            }
        }

        public IEnumerator WaitForBubble(float time)
        { 
            yield return new WaitForSeconds(time);

            EventManager.Broadcast(new CreateBubbleEvent());
        }
    }

    

public class CreateBubbleEvent : IGameEvent
{ 
    // Create bubble
}