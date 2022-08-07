using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private SettingUI settingUI;
    private new Rigidbody rigidbody;
    private Gun gun;
    private GameManager gameManager;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        settingUI = FindObjectOfType<SettingUI>();
        gameManager = FindObjectOfType<GameManager>(); 
       settingUI.OnChangeSetting += OnChangeSetting;
        gun = FindObjectOfType<Gun>();
    }
    public void Update()
    {
        if (rigidbody.velocity.magnitude > 20.0f)
        {
            rigidbody.drag = 5;
        }
        else

        {
            rigidbody.drag = 1;
        }
    }
    public void LateUpdate()
    {
        gameManager.BodyCurrentHeight = transform.position.y;
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
            var f = Vector3.up * collision.relativeVelocity.magnitude;
            f.z = 0.0f;
            rigidbody.AddForce(f, ForceMode.Impulse);
        }
    }

}
