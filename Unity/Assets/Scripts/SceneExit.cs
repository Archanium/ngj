using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public bool allowMouseExit = false;
    public bool allowTriggerExit = true;
    public int sceneIndex = 0;
    public bool is_right = true;
    public SceneNames DestinationScene;
    private EventManager eM;

    private void Start()
    {
        this.eM = EventManager.instance;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && this.allowTriggerExit)
        {
            this.LoadScene();
        }
    }

    private void OnMouseDown()
    {
        if (!this.allowMouseExit || !this.allowTriggerExit)
        {
            return;
        }

        this.LoadScene();
    }

    private void LoadScene()
    {
        this.eM.QueueEvent(new PlayerLockEvent());

        var sceneIndex = (int)this.DestinationScene;
        var sceneIndexString = sceneIndex.ToString();
        this.eM.QueueEvent(new SceneChangeEvent(sceneIndexString));
    }
}

public class FadeEvent : IEvent
{
    private bool fadeIn = false;
    public FadeEvent(bool fadeIn)
    {
        this.fadeIn = fadeIn;
    }
    string IEvent.GetName()
    {
        return this.GetType().ToString();
    }

    object IEvent.GetData()
    {
        return this.fadeIn.ToString();
    }
}
