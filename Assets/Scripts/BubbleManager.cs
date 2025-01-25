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

            BollaModel bollaModel = new BanBolla(1,50,"Horses","Salt");

            EventManager.Broadcast(new CreateBubbleEvent(bollaModel));
        }
    }

    

public class CreateBubbleEvent : IGameEvent
{
    public BollaModel bolla;

    public CreateBubbleEvent(BollaModel bolla)
    {
        this.bolla = bolla;
    }
}