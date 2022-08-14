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
    public void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnItemRestore += GameManager_OnItemRestore;
        
     
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
