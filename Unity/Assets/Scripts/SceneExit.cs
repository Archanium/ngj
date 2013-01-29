using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public bool allowMouseExit = false;
    public bool allowTriggerExit = true;
    public int sceneIndex = 0;
	public bool is_right = true;
	public Collider player;
    public SceneNames DestinationScene;
	private EventManager eM;
	
	private void MovePlayer()
	{
		var x = this.transform.position.x ;
		if(this.is_right) {
			x = x + 190;
		} else {
			x = x - 190;
		}
		this.player.transform.position = 
			new Vector3(
				x,
				this.player.transform.position.y,
				this.player.transform.position.z
			);
		
		GameObject plane = GameObject.Find("player");
		
		(GameObject.Find ("playerBox").GetComponent("MovementController") as MovementController).sceneOffset = this.sceneIndex;
		plane.transform.position = this.player.transform.position;
	}
	private void Start()
	{
		this.eM = EventManager.instance;
		
	}

    private void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Player" && this.allowTriggerExit) {
			this.player = collider;
			this.eM.QueueEvent(new PlayerLockEvent());
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
        this.eM.QueueEvent(new SceneChangeEvent((this.sceneIndex).ToString()));
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
