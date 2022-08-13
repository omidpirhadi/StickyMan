using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeOrth : MonoBehaviour
{
    private float orthoSize;

    public float AspectRate;
    // Update is called once per frame
    void Update()
    {
        orthoSize = AspectRate * Screen.height / Screen.width * 0.5f;
        Camera.main.orthographicSize = orthoSize;
     /// Debug.Log(orthoSize);
     
    }
}
