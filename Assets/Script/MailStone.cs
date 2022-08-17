using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MailStone : MonoBehaviour
{

    public Text mailstone_Text;
    private Transform cam;
    public void Start()
    {

      
        cam = Camera.main.transform;

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

    public void Set_MailStone(string metter, Color c)
    {
        mailstone_Text.text = metter;
        mailstone_Text.material.color = c;
     
    }
}
