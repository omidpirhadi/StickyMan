using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private SettingUI settingUI;
    private new Rigidbody rigidbody;
//    private Gun gun;
    private GameManager gameManager;
   public  Vector3 Lastvelocity;
    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        settingUI = FindObjectOfType<SettingUI>();
        gameManager = FindObjectOfType<GameManager>(); 
       settingUI.OnChangeSetting += OnChangeSetting;
     //   gun = FindObjectOfType<Gun>();
    }
    public void Update()
    {
        if (rigidbody.velocity.magnitude > 20.0f)
        {
            rigidbody.drag = 20;
        }
        else

        {
            rigidbody.drag = 1;
        }
      //  Debug.Log(rigidbody.velocity);
    }
    public void FixedUpdate()
    {
        Lastvelocity = rigidbody.velocity;
    }
    public void LateUpdate()
    {
        //gameManager.BodyCurrentHeight = transform.position.y;
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
            //Debug.Log(rigidbody.velocity.normalized+"vVSsSs"+velocity.normalized);

            var current_velocity = rigidbody.velocity;
            current_velocity.y = 0;
            if (Lastvelocity.y <= 0)
            {

                rigidbody.velocity = current_velocity;
                rigidbody.AddForce(Vector3.up * 400, ForceMode.Impulse);
               // Debug.Log("Force UP");
            }
        }
    }

}
