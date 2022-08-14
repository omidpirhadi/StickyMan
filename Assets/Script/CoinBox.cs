using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    public int Capacity;
    public GameManager gameManager;
    public ParticleSystem particle;
    private Transform cam;
    public void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
      //  cam = Camera.main.transform;
    }
    public void FixedUpdate()
    {
       /* var dis = Vector3.Distance(this.transform.position, cam.position);
        if(dis< 50)
        {
            particle.Play(true);
        }
        else if(dis > 100)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }*/
    }
    private void OnTriggerEnter(Collider t)
    {
        if (t.tag == "Body")
        {

            gameManager.SetCoinvalue(+1);
            Destroy(this.gameObject);
            
        }
    }


  
}
