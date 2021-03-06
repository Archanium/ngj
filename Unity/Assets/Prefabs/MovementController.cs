using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour, IEventListener {
	
	public GameObject plane;
	public Texture[] textures;
	public AudioClip[] footsteps;
	
	
	private float speed = 100f;
	private float rotation = 0f;
	private int textureNumber = 0;
	private int walkLoop = 0;
	private int walkLoopDelay = 0;
	private int idleLoop = 0;
	private int idleLoopDelay = 0;
	private float horMovement = 0f;
	private int idleDir = 1;
	public int sceneOffset = 0;
	public bool movable = true;
	private float desiredWidth;
	
	
	void Awake()
	{
		EventManager.instance.AddListener(this as IEventListener, "PlayerLockEvent");
		EventManager.instance.AddListener(this as IEventListener, "PlayerUnlockEvent");	
	}
	// Use this for initialization
	void Start () {
		plane = GameObject.Find("player");
		plane.transform.position = transform.position;
	}
	
	bool IEventListener.HandleEvent(IEvent e)
	{
		//print(e.GetName());
		if(e.GetName() == "PlayerLockEvent") {
			this.movable = false;	
		} else if (e.GetName() == "PlayerUnlockEvent") {
			this.movable = true;
		}
		return true;
	}
	
	// Update is called once per frame
	void Update () {
		horMovement = Input.GetAxis("Horizontal");
		if (horMovement == 0) {
			idleLoopDelay = idleLoopDelay + 1;
			if (idleLoopDelay % 75 == 0) {
				idleLoop = idleLoop + (idleDir);
			}
			textureNumber = idleLoop;
			if (idleLoop == 2) {
				idleDir = -1;
			}
			if (idleLoop == 0) {
				idleDir = 1;
			}
		} else {
			idleLoopDelay = 0;
			idleLoop = 0;
			idleDir = 1;
		}
 	
		if (horMovement < 0) {
			rotation = 0;
		}
		if (horMovement > 0) {
			rotation = 180;
		}
		if (Mathf.Abs(horMovement) > 0.1) {
			textureNumber = 1;
		}
		if (Mathf.Abs(horMovement) > 0.5) {
			textureNumber = 2;
		}
		if (Mathf.Abs(horMovement) == 1) {
			//Camera.main.orthographicSize = 256;    
			walkLoopDelay = walkLoopDelay + 1;
			if (walkLoop == 6 || walkLoop == 3) {
				playRandomSounds();
			}
			if (walkLoopDelay % 25 == 0) {
				walkLoop = walkLoop + 1;
			}
			textureNumber = walkLoop + 2;
			if (walkLoop > 7) {
				walkLoop = 2;
				walkLoopDelay = 0;
			}
		} else {
			//Camera.main.orthographicSize = 768;
			walkLoopDelay = 0;
			walkLoop = 0;
			textureNumber = 2;
		}
		if (textureNumber > 3) {
			if(movable) {
				transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
			}
			
			plane.transform.position = transform.position;
		}
		plane.renderer.sharedMaterial.mainTexture = textures[textureNumber];
		plane.transform.rotation = Quaternion.Euler(0,rotation,0);
		
		desiredWidth = 1920f - (1920f - 1400f) * Mathf.Abs(horMovement);
		var currentWidth = Screen.width;
		var currentHeight = Screen.height;
		var size = Mathf.Round( ( ( this.desiredWidth / currentWidth ) * currentHeight ) / 2.0f);
		size -= size % 2.0f;
		Camera.main.orthographicSize = size;
		
		Camera.main.transform.position = new Vector3(transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z);
		
		if ((Camera.main.transform.position.x + 960 + desiredWidth/2) > (1920 * (sceneOffset + 1))) {
			Camera.main.transform.position = new Vector3((960 - desiredWidth/2 + (1920 * sceneOffset)),Camera.main.transform.position.y,Camera.main.transform.position.z);
		}
		if ((Camera.main.transform.position.x + 960 - desiredWidth/2) < (1920 * (sceneOffset))) {
			Camera.main.transform.position = new Vector3((desiredWidth/2 - 960 + (1920 * sceneOffset)),Camera.main.transform.position.y,Camera.main.transform.position.z);
		}
	}
	
	private void playRandomSounds () {
		audio.clip = footsteps[Random.Range(0,footsteps.Length)];
    	audio.Play();
	}

}