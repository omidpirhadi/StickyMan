using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    public FXV.FXVShield fXVShield;
    public float DurationSheild = 20;
    public bool IsSheided = false;
    private new Rigidbody rigidbody;

    private GameManager gameManager;
   public  Vector3 Lastvelocity;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    
        gameManager = FindObjectOfType<GameManager>();
        fXVShield.SetShieldActive(false, false);

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
  
    public void ActiveShield()

    {
       // Debug.Log("on");
        fXVShield.SetShieldActive(true,true);
        IsSheided = true;
        DG.Tweening.DOVirtual.DelayedCall(10, () => {
            fXVShield.SetShieldActive(false,true);
            IsSheided = false;
           // Debug.Log("off");
        });
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
