using GameStatus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility.GameEventManager;


public class BubbleManager : MonoBehaviour
{
    public BollaPanel BollaPanelPrefab;
    public Transform canvasTransform;

    public List<float> bubbleTimes;

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
        foreach (var bubbleTime in bubbleTimes)
        {
            StartCoroutine(WaitForBubble(bubbleTime));
        }
    }

    public IEnumerator WaitForBubble(float time)
    {
        yield return new WaitForSeconds(time);

        BollaModel bollaModel = GetRandomBolla();


        BollaPanel bollaPanel = Instantiate(BollaPanelPrefab, canvasTransform);
        bollaPanel.Initialize(bollaModel.getDescription());
        EventManager.Broadcast(new CreateBubbleEvent(bollaModel));
    }

    public BollaModel GetRandomBolla()
    {
        int ran = UnityEngine.Random.Range(0, 2);
        if (ran == 0)
            return BanBolla.Build();
        else
            return ResourceNumberBolla.Build();
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