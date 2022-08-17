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
    private Transform cam;
    public void Start()
    {
       
        gun = FindObjectOfType<Gun>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnItemRestore += GameManager_OnItemRestore;
        cam = Camera.main.transform;
        Isused = false;
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
