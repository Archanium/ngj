using UnityEngine;

public class Taxi : MonoBehaviour
{
    public Material[] materials;
    public float position;
    public float speed = 1.0f;

    private void Update()
    {
        var materials = this.materials;
        if (materials == null)
        {
            return;
        }

        this.position += Time.deltaTime * this.speed;
        var index = ((int)this.position) % this.materials.Length;
        var material = this.materials[index];
        this.renderer.material = material;
    }
}
