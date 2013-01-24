using UnityEngine;

public class DrunkFightAudio : MonoBehaviour
{
    public AudioSource drunksAudioSource;
    public AudioSource laughAudioSource;
    private bool fadingOut;

    private void OnTriggerEnter(Collider collider)
    {
        if (!this.enabled)
        {
            return;
        }

        var drunksAudioSource = this.drunksAudioSource;
        if (drunksAudioSource == null)
        {
            return;
        }

        if (drunksAudioSource.isPlaying)
        {
            return;
        }

        this.fadingOut = false;

        drunksAudioSource.volume = 1.0f;
        drunksAudioSource.Play();
    }

    private void OnTriggerExit(Collider collider)
    {
        var drunksAudioSource = this.drunksAudioSource;
        if (drunksAudioSource == null)
        {
            return;
        }

        this.fadingOut = true;
    }

    private void Update()
    {
        if (!this.fadingOut)
        {
            return;
        }

        var drunksAudioSource = this.drunksAudioSource;
        if (drunksAudioSource == null)
        {
            return;
        }

        drunksAudioSource.volume -= Time.deltaTime;
        if (drunksAudioSource.volume <= 0.0f)
        {
            this.fadingOut = false;
            drunksAudioSource.Stop();

            var laughAudioSource = this.laughAudioSource;
            if (laughAudioSource == null)
            {
                return;
            }

            this.laughAudioSource.Play();
        }
    }
}
