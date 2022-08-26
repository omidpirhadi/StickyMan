using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHealth : MonoBehaviour
{
    public bool PlatformDamageForHume = false;
    public GameObject BodyBloodEffect;
    public GameObject Parent;
    public float Health = 100;

    public new  MeshRenderer renderer;
   
    public GameObject RayFired_Gameobject;
    public List<GameObject> Additionl = new List<GameObject>();
    public new Animation animation;
        
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
                if (animation)
                    animation.Stop();
                GetComponent<Collider>().enabled = false;
                renderer.enabled = false;
                Additionl.ForEach(e => {
                    e.AddComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere * 20, ForceMode.Impulse);
                });
                RayFired_Gameobject.SetActive(true);
                
                Destroy(Parent, 5);
                Debug.Log("PlatformCracked!");
            }

        }
        if (collision.collider.tag == "Body" &&  PlatformDamageForHume == true)
        {
            if (!collision.gameObject.GetComponent<Body>().IsSheided)
            {
                collision.rigidbody.isKinematic = true;
                var e = Instantiate(BodyBloodEffect, collision.contacts[0].point, Quaternion.identity);

                DG.Tweening.DOVirtual.DelayedCall(0.5f, () =>
                {
                    Destroy(e);
                    FindObjectOfType<GameManager>().CompeletRecord();


                });

                Debug.Log("boddy dead");
            }
        }
    }
}
