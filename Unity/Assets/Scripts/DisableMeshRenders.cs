using UnityEngine;

/// <summary>
/// Disables mesh renders on all children.
/// </summary>
public class DisableMeshRenders : MonoBehaviour
{
    private void Start()
    {
        for (var childIndex = 0; childIndex < this.transform.childCount; childIndex++)
        {
            var child = this.transform.GetChild(childIndex);
            child.renderer.enabled = false;
        }
    }
}
