using System;
using UnityEngine;

public class NeonlightRandomBigBuzz : MonoBehaviour, IEventListener
{
    public AudioSource audioSource;
    public float minimumDelay = 10.0f;
    public float chanceOfPlay = 0.05f;
    public SceneNames playInScene;

    private SceneNames currentScene = SceneNames.Menu;
    private float delay = 0.0f;

    private void Awake()
    {
        EventManager.instance.AddListener(this, "SceneChangeEvent");
    }

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

        if (this.playInScene != this.currentScene)
        {
            return;
        }

        if (UnityEngine.Random.Range(0.0f, 1.0f) > this.chanceOfPlay)
        {
            return;
        }

        if (!audioSource.enabled)
        {
            return;
        }

        audioSource.Play();
        this.delay = this.minimumDelay;
    }

    public bool HandleEvent(IEvent @event)
    {
        var sceneChangeEvent = @event as SceneChangeEvent;
        if (sceneChangeEvent == null)
        {
            return true;
        }

        var data = @event.GetData() as string;
        if (data == null)
        {
            return true;
        }

        try
        {
            this.currentScene = (SceneNames)Enum.Parse(typeof(SceneNames), data, ignoreCase: true);
        }
        catch
        {
            // do nothing
        }

        return true;
    }
}
