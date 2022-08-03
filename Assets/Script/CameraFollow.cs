using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float SpeedFollow = 0.5f;
    public float Y_Min = 6.0f;

    public Vector3 FirstPosCam;
    public float MaxDistance = 5;
    public float temp_dis;
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Humen").transform;
        FirstPosCam = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Body").transform;

        }
        temp_dis = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(target.position.x, target.position.y, 0));
        if ( target.position.y > Y_Min)
        {
            transform.DOMove(new Vector3(transform.position.x, target.position.y, transform.position.z), SpeedFollow);

        }
        else
        {
            transform.DOMove(FirstPosCam, 0.5f);
        }

    }

}
