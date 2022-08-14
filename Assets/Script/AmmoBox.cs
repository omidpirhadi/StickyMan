using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public int Capacity;
    public Gun gun;
    public GameObject Effect;
    private GameManager gameManager;
    private bool Isused = false;

    public void Start()
    {
       
        gun = FindObjectOfType<Gun>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnItemRestore += GameManager_OnItemRestore;

        Isused = false;
    }

    private void GameManager_OnItemRestore(bool restore)
    {
        if(restore)
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
             Effect.SetActive(false);
            gun.Ammo(+Capacity);
            
           
        }
    }
}
