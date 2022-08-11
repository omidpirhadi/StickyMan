using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowT2 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 0.123f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newpos = target.position + offset;

        var newpos_smooth = Vector3.Lerp(transform.position, newpos, speed);
        transform.position = newpos_smooth;
    }
}
