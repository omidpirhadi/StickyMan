using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Body")
        {
            Debug.Log("Winner");
        }
    }
}
