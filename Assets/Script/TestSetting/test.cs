﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed, Space.World);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed, Space.World);
        }

    }
}
