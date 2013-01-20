public class SceneChangeEvent : IEvent {
	
	private string sceneIndex;
	
	public SceneChangeEvent(string sceneIndex)
	{
		this.sceneIndex = sceneIndex;
	}
	
	string IEvent.GetName()
    {
        return this.GetType().ToString();
    }
 
    object IEvent.GetData()
	{
		return this.sceneIndex as object;
	}
}