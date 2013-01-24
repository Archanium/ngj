using UnityEngine;

public class ManageSceneAudioSource : MonoBehaviour
{
    public Transform cameraTransformation;
    public float enableOnPositionX;
    public SceneNames Scene;
    public AudioSource audioSource;

    private void Update()
    {
        var camera = this.cameraTransformation;
        if (camera == null)
        {
            return;
        }

        var audioSource = this.audioSource;
        if( audioSource == null )
        {
            return;
        }

        //var enabled = camera.position.x == this.enableOnPositionX;
        var enabled = SceneManager.CurrentScene == this.Scene;
        audioSource.enabled = enabled;
    }
}
