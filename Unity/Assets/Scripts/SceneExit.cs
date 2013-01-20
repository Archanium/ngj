using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public bool allowMouseExit = false;
    public bool allowTriggerExit = true;
    public int sceneIndex = 0;

    private void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Player" && this.allowTriggerExit) {
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
        var transform = Camera.mainCamera.transform;
        transform.position = new Vector3(
            this.sceneIndex * 1920,
            transform.position.y,
            transform.position.z
            );
    }
}
