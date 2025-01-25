using Utility.GameEventManager;

public class AddNewCantoneEvent : IGameEvent
{
    public PlayerModel playerModel;

    public AddNewCantoneEvent(PlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }
}

