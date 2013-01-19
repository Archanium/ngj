using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private float initialIntensity;
    private float delay;
    public float range = 4.0f;
    public float frequency = 1.0f;

    private void Start()
    {
        this.initialIntensity = this.light.intensity;
    }

    private void Update()
    {
        this.delay += Time.deltaTime;
        if (this.delay < this.frequency)
        {
            return;
        }

        this.delay = 0.0f;
        this.light.intensity = this.initialIntensity * Random.Range(-this.range, this.range);
    }
}
