using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody Bullet;
    public Transform BulletPlace;
    public float PowerFire;
    public ForceMode forceMode;
    private new Rigidbody rigidbody;

    public void Fire()
    {
        
        var b = Instantiate(Bullet, BulletPlace.position, Quaternion.identity);

        b.AddForce(transform.up * PowerFire, forceMode);
        
    }
}
