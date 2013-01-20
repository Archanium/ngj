public class PlayerUnlockEvent : IEvent {
	public PlayerUnlockEvent(){}
	
	string IEvent.GetName()
    {
        return this.GetType().ToString();
    }
 
    object IEvent.GetData()
	{
		return this.GetType().ToString() as object;
	}
}