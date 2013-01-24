using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour, IEventListener
{
	private int index = 0;
	private int endSceneStart = 3;
	private int endSceneEnd = 4;
	private float delay = 5;
	private bool shown = false;
	// Use this for initialization
	void Start ()
	{
		EventManager.instance.AddListener(this as IEventListener, "SceneChangeEvent");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(index == endSceneStart && shown) {
			delay -= Time.deltaTime;
			if(delay < 0) {
				EventManager.instance.QueueEvent(new SceneChangeEvent(endSceneEnd.ToString()));	
			}
		}
	}
	
	bool IEventListener.HandleEvent(IEvent e) 
	{
		if(e.GetName() == "SceneChangeEvent") {
			this.index = int.Parse(e.GetData() as string);
			print (this.index);
			EventManager.instance.AddListener(this as IEventListener, "FadeEvent");
			EventManager.instance.QueueEvent(new PlayerLockEvent());
			EventManager.instance.QueueEvent(new FadeOutEvent(2));
			if(index < endSceneStart) {
				this.TransformPlayer(this.index);
			}
			
		}
		if(e.GetName() == "FadeEvent") {
			if(bool.TrueString == e.GetData() as string) {
				this.TransformCamera(this.index);
				EventManager.instance.QueueEvent(new FadeInEvent(2));
			} else if (bool.FalseString == e.GetData() as string) {
				EventManager.instance.DetachListener(this as IEventListener, "FadeEvent");
				EventManager.instance.QueueEvent(new PlayerUnlockEvent());
				
				if(index == endSceneStart) {
					this.shown = true;
				}
			}
		}
		
		return true;
	}
	
	private void TransformCamera(int index) 
	{
		var transform = Camera.mainCamera.transform;
		transform.position = new Vector3(
	            1920*index,
	            transform.position.y,
	            transform.position.z
	            );
		(GameObject.Find ("playerBox").GetComponent("MovementController") as MovementController).sceneOffset = index;
	}
	
	private void TransformPlayer(int index) 
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject p in players) {
			var transform = p.transform;
			transform.position = new Vector3(
	            1920*index-780,
	            transform.position.y,
	            transform.position.z
	            );
		}
	}
}

