using UnityEngine;

/// <summary>
/// Disables mesh renders on all decendants.
/// </summary>
public class DisableMeshRenders : MonoBehaviour
{
    private void Start()
    {
        this.Recursive(this.transform);
        this.enabled = false;
    }

    private void Recursive(Transform transform)
    {
        for (var childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            var child = transform.GetChild(childIndex);
            if (child == null)
            {
                continue;
            }

            var renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            this.Recursive(child);
        }
    }
}
