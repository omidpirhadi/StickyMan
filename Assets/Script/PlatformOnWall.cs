using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOnWall : MonoBehaviour
{
    private Transform cam;
    public void Start()
    {


        cam = Camera.main.transform;
    
    }
    // Update is called once per frame
    public void LateUpdate()
    {
        var temp_y = cam.transform.position.y - this.transform.position.y;
        if (temp_y > 50)
        {

            //Debug.Log("Des" + gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
