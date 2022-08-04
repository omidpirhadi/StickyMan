using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private SettingUI settingUI;
    private new Rigidbody rigidbody;
    private Gun gun;
    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        settingUI = FindObjectOfType<SettingUI>();
        settingUI.OnChangeSetting += OnChangeSetting;
        gun = FindObjectOfType<Gun>();
    }

    private void OnChangeSetting()
    {
        rigidbody.mass = settingUI.massbody;
        rigidbody.drag = settingUI.dragbody;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            //Debug.Log("CCCCCCCCCCCCCCCCCCCCCC");
            //var nor = collision.contacts[0].normal;
           // var point = collision.contacts[0].point;
          //  var dir = (point - transform.position).normalized;
            //rigidbody.AddForce(Vector3.up * collision.relativeVelocity.magnitude, ForceMode.Impulse);
            rigidbody.AddForce(Vector3.up * collision.relativeVelocity.magnitude, ForceMode.Impulse);
        }
    }

}
