using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    public int Capacity;
    public Diaco.Cannonman.UI.GameHUD gameHUD_ui;

    public GameObject Effect;

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

    private void OnTriggerEnter(Collider t)
    {
        if (gameHUD_ui == null)
        {
            gameHUD_ui = FindObjectOfType<Diaco.Cannonman.UI.GameHUD>();
        }
        if (t.tag == "Body")
        {
            Effect.SetActive(false);
            if (gameHUD_ui)
                gameHUD_ui.SetCoinvalue(+1);

            Destroy(this.gameObject);
        }

    }



}
