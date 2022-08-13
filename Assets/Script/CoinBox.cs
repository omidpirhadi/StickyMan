using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    public int Capacity;
    public GameManager gameManager;
    public void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider t)
    {
        if (t.tag == "Body")
        {

            gameManager.SetCoinvalue(+1);
            Destroy(this.gameObject);
        }
    }
}
