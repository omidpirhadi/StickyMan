using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float speed;
    public float pow_exppl = 10;
    public float radius_exppl = 10;
    public float upward_exppl = 10;
    public ForceMode force;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddExplosionForce(pow_exppl, transform.position, radius_exppl, upward_exppl, force);
        }
        if (Input.GetKeyDown(KeyCode.W))
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
