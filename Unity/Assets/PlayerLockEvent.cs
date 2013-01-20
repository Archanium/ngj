public class PlayerLockEvent : IEvent {
	public PlayerLockEvent(){}
	
	string IEvent.GetName()
    {
        return this.GetType().ToString();
    }
 
    object IEvent.GetData()
	{
		return this.GetType().ToString() as object;
	}
}