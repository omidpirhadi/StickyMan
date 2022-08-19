using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public int Capacity;
    public Gun gun;
    public GameObject Effect;
    public Diaco.Cannonman.UI.GameHUD gameHUD_ui;
    private bool Isused = false;
    private Transform cam;
    public void Start()
    {
       
        gun = FindObjectOfType<Gun>();
        gameHUD_ui = FindObjectOfType<Diaco.Cannonman.UI.GameHUD>();
       
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
        if (gameHUD_ui == null)
        {
            gameHUD_ui = FindObjectOfType<Diaco.Cannonman.UI.GameHUD>();
        }
        if (t.tag == "Body" && Isused == false)
        {
            if (gameHUD_ui)
                gameHUD_ui.SetAmmoText();
            Effect.SetActive(false);
            gun.Ammo(+Capacity);

            Destroy(this.gameObject);
        } 
    }
}
