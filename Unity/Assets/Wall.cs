using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Player") {
			var horMovement = Input.GetAxis("Horizontal");
			collider.transform.position = new Vector3(collider.transform.position.x + -1 * horMovement * 35, collider.transform.position.y, collider.transform.position.z);
		}
	}
}
