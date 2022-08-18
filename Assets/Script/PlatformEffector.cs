using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{
    public  float PowerPlatform = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Body")
        {
            var dir = collision.collider.GetComponent<Body>().Lastvelocity - collision.contacts[0].point.normalized;
           // var reflect = Vector3.Reflect(dir, collision.contacts[0].normal);
            collision.rigidbody.AddForce(transform.up* PowerPlatform, ForceMode.Impulse);
            Debug.Log("Effector");
        }
    }
}
