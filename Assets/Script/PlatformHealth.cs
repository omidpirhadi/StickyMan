using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHealth : MonoBehaviour
{
    public float Health = 100;
    public List<GameObject> Addtionalpart;
    private RayFire.RayfireRigid rayfire;

    private void Start()
    {
        rayfire = GetComponent<RayFire.RayfireRigid>();
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
                Addtionalpart.ForEach((e) => {
                    e.AddComponent<Rigidbody>().AddForce(UnityEngine.Random.onUnitSphere * 10, ForceMode.Impulse);
                    
                });
                rayfire.Initialize();
                rayfire.Activate();
                rayfire.Demolish();
                Debug.Log("PlatformCracked!");
            }

        }
    }
}
