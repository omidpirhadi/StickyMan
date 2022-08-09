using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public ParticleSystem explosionEffet;
   private Transform cam;

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
           
            var effect = Instantiate(explosionEffet, collision.contacts[0].point, explosionEffet.transform.rotation);
            effect.Play(true);
            Destroy(this.gameObject, 0.1f);
           // Debug.Log("A");
        }
    }
}
