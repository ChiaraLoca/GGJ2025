using GameStatus;
using UnityEngine;

using Utility.GameEventManager;

public class MessageManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.AddListener<SendResponseEvent>(OnSendResponse);
    }

    private void OnSendResponse(SendResponseEvent evt)
    {
        int randomeResurce = UnityEngine.Random.Range(0, 4);
        int randomMore = UnityEngine.Random.Range(0, 2);

        (string Description, string Type) res = GameModel.Risorse[randomeResurce];

        if (randomMore == 1)
        {
            int max = int.MinValue;
            PlayerModel maxPlayerModel = null;
            foreach (PlayerModel playerModel in GameStatusManager.instance.Players)
            {
                if (playerModel.Score.getResource(res.Type) > max)
                {
                    max = playerModel.Score.getResource(res.Type);
                    maxPlayerModel = playerModel;
                }
            }

            EventManager.Broadcast(new AddMessageEvent("Il cantone con il maggior numero della risorsa " + res.Description + " è " + maxPlayerModel.Name));
        }
        else 
        {
            int min = int.MaxValue;
            PlayerModel minPlayerModel = null;
            foreach (PlayerModel playerModel in GameStatusManager.instance.Players)
            {
                if (playerModel.Score.getResource(res.Type) < min)
                {
                    min = playerModel.Score.getResource(res.Type);
                    minPlayerModel = playerModel;
                }
            }

            EventManager.Broadcast(new AddMessageEvent("Il cantone con il minor numero della risorsa " + res.Description + " è " + minPlayerModel.Name));
        }

            

    }
}

