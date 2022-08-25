using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiliyGravityBox : MonoBehaviour
{

    public float DurationAbility = 5;
    public GameObject Effect;
    private GameManager gameManager;
    private Transform cam;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        
        if (t.tag == "Body")
        {
            gameManager.AbillityGravity.SetActive(true);
            Effect.SetActive(false);

           // Debug.Log("abillittttttt");
            Destroy(this.gameObject);
        }

    }
}
