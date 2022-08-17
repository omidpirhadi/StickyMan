using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }




    public void LateUpdate()
    {

        var temp_y = cam.transform.position.y - this.transform.position.y;
        if (temp_y > 115)
        {
            var temp_pos = this.transform.position;
            temp_pos.y += 1000;
            this.transform.position = temp_pos;

        }
    }
}
