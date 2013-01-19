using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float movement = -1.0f;

    private void Update()
    {
		this.transform.position = new Vector3(this.transform.position.x + this.movement * Time.deltaTime, this.transform.position.y, this.transform.position.z);
    }
}
