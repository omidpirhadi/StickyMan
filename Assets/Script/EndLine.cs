using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    private Body body;
    private void Start()
    {
        body = FindObjectOfType<Body>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Body")
        {
            body.GetComponent<Rigidbody>().Sleep();
            body.GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Winner");
        }
    }
}
