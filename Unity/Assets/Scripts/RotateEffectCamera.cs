using UnityEngine;

public class RotateEffectCamera : MonoBehaviour
{
    public float offset;

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Cos(Time.time + offset) * 3.0f);
    }
}
