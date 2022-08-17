using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    public int Capacity;
    public GameManager gameManager;
    
    public GameObject Effect;
    private bool Isused = false;
    private Transform cam;
    public void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnItemRestore += GameManager_OnItemRestore;
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
    private void GameManager_OnItemRestore(bool restore)
    {
        if (restore)
        {
            Isused = false;
            Effect.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider t)
    {
        if (t.tag == "Body" && Isused == false)
        {
            Isused = true;
            gameManager.SetCoinvalue(+1);
            Effect.SetActive(false);

        }
    }


  
}
