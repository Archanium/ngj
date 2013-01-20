using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	public int x = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		print(collision.collider.transform.position.ToString());
		collision.collider.transform.position = new Vector3(
			collision.collider.transform.position.x+10,
			collision.collider.transform.position.y,
			collision.collider.transform.position.z
		);
		
	}
}
