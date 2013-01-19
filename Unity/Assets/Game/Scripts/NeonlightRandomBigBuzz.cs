using UnityEngine;

public class NeonlightRandomBigBuzz : MonoBehaviour
{
    public AudioSource audioSource;
    public float minimumDelay = 10.0f;
    public float chanceOfPlay = 0.05f;

    private float delay = 0.0f;

    private void Update()
    {
        var audioSource = this.audioSource;
        if (audioSource == null)
        {
            return;
        }
        if (audioSource.isPlaying)
        {
            return;
        }

        this.delay -= Time.deltaTime;
        if (this.delay > 0.0f)
        {
            return;
        }

        if (Random.Range(0.0f, 1.0f) > this.chanceOfPlay)
        {
            return;
        }

        audioSource.Play();
        this.delay = this.minimumDelay;
    }
}
