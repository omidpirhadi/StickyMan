using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MailStone : MonoBehaviour
{

    public Text mailstone_Text;
    public Material Yellow_color, white_color;
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

    public void Set_MailStone(string metter, string color)
    {
        if (color == "w")
        {
            mailstone_Text.text = metter;
            mailstone_Text.material = white_color;
        }
        else if(color == "y")
        {
            mailstone_Text.text = metter;
            mailstone_Text.material = Yellow_color;
        }
     
    }
}
