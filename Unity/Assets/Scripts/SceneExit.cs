using UnityEngine;

public class SceneExit : MonoBehaviour, IEventListener
{
    public bool allowMouseExit = false;
    public bool allowTriggerExit = true;
    public int sceneIndex = 0;
	public Component player;
	private EventManager eM;
	
	public bool HandleEvent(IEvent e) 
	{
		if(bool.TrueString == e.GetData() as string) {
			this.TransformCamera();
			this.eM.QueueEvent(new FadeInEvent(3));
			this.eM.DetachListener(this as IEventListener, "FadeEvent");
		}
		return true;
	}

	
	private void Start()
	{
		this.eM = EventManager.instance;
		
	}

    private void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Player" && this.allowTriggerExit) {
			this.LoadScene();	
		}
    }

    private void OnMouseDown()
    {
        if (!this.allowMouseExit || !this.allowTriggerExit)
        {
            return;
        }

        this.LoadScene();
    }
	
    private void LoadScene()
    {
		this.eM.AddListener(this as IEventListener, "FadeEvent");
		this.eM.QueueEvent(new FadeOutEvent(2));
    }
	
	private void TransformCamera() 
	{
		var transform = Camera.mainCamera.transform;
        transform.position = new Vector3(
            this.sceneIndex * 1920,
            transform.position.y,
            transform.position.z
            );
	}
}
	
public class FadeEvent : IEvent
	{
		private bool fadeIn = false;
		public FadeEvent(bool fadeIn)
		{
			this.fadeIn = fadeIn;
		}
		string IEvent.GetName()
	    {
	        return this.GetType().ToString();
	    }
 
	    object IEvent.GetData()
	    {
	        return this.fadeIn.ToString();
	    }		
	}
