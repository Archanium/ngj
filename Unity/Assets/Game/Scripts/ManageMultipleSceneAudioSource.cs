using System;
using System.Linq;
using UnityEngine;

public class ManageMultipleSceneAudioSource : MonoBehaviour, IEventListener
{
    public SceneNames[] Scenes;
    public AudioSource audioSource;
    private SceneNames currentScene = SceneNames.StreetClub;

    private void Awake()
    {
        EventManager.instance.AddListener(this, "SceneChangeEvent");
    }

    private void Update()
    {
        var audioSource = this.audioSource;
        if( audioSource == null )
        {
            return;
        }

        //Debug.Log(this.currentScene);
        var enabled = this.Scenes.Any( s => s == this.currentScene );
        audioSource.enabled = enabled;
    }

    public bool HandleEvent(IEvent @event)
    {
        //Debug.LogWarning("change scene event");

        var sceneChangeEvent = @event as SceneChangeEvent;
        if (sceneChangeEvent == null)
        {
            Debug.LogError("SceneChangeEvent is null");
            return true;
        }

        var data = @event.GetData() as string;
        if (data == null)
        {
            Debug.LogError("data is null");
            return true;
        }

        try
        {
            this.currentScene = (SceneNames)Enum.Parse(typeof(SceneNames), data, ignoreCase: true);
            //Debug.LogWarning("change scene to " + this.currentScene );
        }
        catch
        {
            //Debug.Log("unable to parse");
            // do nothing
        }

        return true;
    }
}
