using UnityEngine;

public class StartGame : MonoBehaviour
{
    public SceneNames nextScene = SceneNames.StreetClub;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.enabled = false;

            var sceneChanger = GameObject.FindObjectOfType(typeof(SceneChanger)) as SceneChanger;
            sceneChanger.nextPlayerOffset = 548.0f + 780.0f;

            var sceneIndex = (int)this.nextScene;
            var sceneIndexString = sceneIndex.ToString();

            EventManager.instance.QueueEvent(new SceneChangeEvent(sceneIndexString));
        }
    }
}
