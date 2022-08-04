using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        var dis = Vector3.Distance(transform.position, cam.position);
        if(dis>80)
        {
            Destroy(this.gameObject);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Body" || collision.collider.tag == "Wall")
        {
           // Destroy(this.gameObject, 0.0f);
           // Debug.Log("A");
        }
    }
}
