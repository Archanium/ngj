using UnityEngine;

public class CreepyTaxiDriverGuy : MonoBehaviour
{
    public Transform player;
    public float minimumDistance = 100.0f;
    private float initialPositionX;

    private void Start()
    {
        this.initialPositionX = this.transform.position.x;
    }

    private void Update()
    {
        var player = this.player;
        if (player == null)
        {
            return;
        }

        var distance = player.position.x - this.transform.position.x;

        if (distance > -10 && distance < 10)
        {
            return;
        }

        var position = 0f;
        // Right
        if (distance > 0)
        {
            position = initialPositionX - Mathf.Lerp(0f, this.minimumDistance, Mathf.Abs(distance) / this.minimumDistance);
        }
        else if (distance < 0)
        {
            position = initialPositionX + Mathf.Lerp(0f, this.minimumDistance,Mathf.Abs(distance)/this.minimumDistance);
        }
        else
        {
            position = this.transform.position.x;
        }


        this.transform.position = new Vector3(position, this.transform.position.y, this.transform.position.z);
        //print(this.transform.position);
    }
}
