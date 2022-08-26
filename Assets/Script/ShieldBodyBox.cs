using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBodyBox : MonoBehaviour
{
    public GameObject Effect;
    private Body body;
    private Transform cam;
    public void Start()
    {

       
   
        cam = Camera.main.transform;
        // Isused = false;
    }
    public void LateUpdate()
    {
        var temp_y = cam.transform.position.y - this.transform.position.y;
        if (temp_y > 50)
        {

            //Debug.Log("Des" + gameObject.name);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider t)
    {
        if (body == false)
            body = FindObjectOfType<Body>();
        if (t.tag == "Body" )
        {
          
           
                body.ActiveShield();


            Effect.SetActive(false);
            Destroy(this.gameObject);
           // Debug.Log("AAAammm");
        }
    }
}
