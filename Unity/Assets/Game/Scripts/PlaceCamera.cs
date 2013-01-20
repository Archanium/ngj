using System;
using UnityEngine;

public class PlaceCamera : MonoBehaviour
{
    public float desiredWidth = 1920.0f;
    public Camera targetCamera;

    private void FixedUpdate()
    {
        var targetCamera = this.targetCamera;
        if (targetCamera == null)
        {
            return;
        }

        //var resolution = Screen.currentResolution;
        //var currentWidth = (float)resolution.width;
        //var currentHeight = (float)resolution.height;
        var currentWidth = (float)Screen.width;
        var currentHeight = (float)Screen.height;
        var size = Mathf.Round( ( ( this.desiredWidth / currentWidth ) * currentHeight ) / 2.0f );
        size -= size % 2.0f;
        targetCamera.orthographicSize = size;
    }
}
