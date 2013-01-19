using UnityEngine;

public class MeatOrgyJumping : MonoBehaviour
{
    public float force = 1.0f;
    public float speed = 1.0f;
    private float initialPositionY;
    public float maxJumpHeight = 50.0f;
    private float currentForce = 0.0f;

    private void Awake()
    {
        this.initialPositionY = this.transform.position.y;
    }

    private void Update()
    {
        if (this.initialPositionY == this.transform.position.y)
        {
            this.currentForce = this.force * Random.Range(0.1f, 1.0f);
        }

        this.transform.position = new Vector3(
            this.transform.position.x,
            Mathf.Clamp(this.transform.position.y + this.currentForce * Time.deltaTime * this.speed, this.initialPositionY, this.initialPositionY + this.maxJumpHeight),
            this.transform.position.z
            );

        this.currentForce -= 9.0f * Time.deltaTime;
    }
}
