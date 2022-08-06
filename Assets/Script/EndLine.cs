using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EndLine : MonoBehaviour
{

    public bool IsDeadLine = false;
    private GameManager gameManager;
    private Body body;
    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        body = FindObjectOfType<Body>();
        gameManager = FindObjectOfType<GameManager>();
        if (other.tag == "Body")
        {
            DOVirtual.DelayedCall(1, () =>
            {
                body.GetComponent<Rigidbody>().Sleep();
                body.GetComponent<Rigidbody>().isKinematic = true;
            });
  

            if (!IsDeadLine)
            {

               
                gameManager.LevelCompeleted();
                Debug.Log("Winner");
            }
            else
            {
        

                gameManager.LevelFail();
                Debug.Log("LOSER");
            }
        }
    }
}
