using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHealth : MonoBehaviour
{
    public float Health = 100;
    public new  MeshRenderer renderer;
   
    public GameObject RayFired_Gameobject;
    public List<GameObject> Additionl = new List<GameObject>();
    private void Start()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            if (Health > 0)
            {
                Health -= 10;
                Debug.Log("HEALTH:" + Health);
            }
            else
            {
                GetComponent<Collider>().enabled = false;
                renderer.enabled = false;
                Additionl.ForEach(e => {
                    e.AddComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere * 20, ForceMode.Impulse);
                });
                RayFired_Gameobject.SetActive(true);
                
                Destroy(this, 5);
                Debug.Log("PlatformCracked!");
            }

        }
    }
}
