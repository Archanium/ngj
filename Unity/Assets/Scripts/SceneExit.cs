using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public bool allowMouseExit = false;
    public string sceneName = string.Empty;

    private void OnTriggerEnter(Collider collider)
    {
        this.LoadScene();
    }

    private void OnMouseDown()
    {
        if (!this.allowMouseExit)
        {
            return;
        }

        this.LoadScene();
    }

    private void LoadScene()
    {
        if (string.IsNullOrEmpty(this.sceneName))
        {
            return;
        }

        Application.LoadLevel(this.sceneName);
    }
}
