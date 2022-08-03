using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public int Capacity;
    public Gun gun;
    public void Start()
    {
       
        gun = FindObjectOfType<Gun>();
    }
    private void OnTriggerEnter(Collider t)
    {
        if (t.tag == "Body")
        {
            
            gun.Ammo(+Capacity);
            Destroy(this.gameObject);
        }
    }
}
