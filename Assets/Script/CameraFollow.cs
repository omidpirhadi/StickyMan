using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float SpeedFollow = 0.5f;


    public float Paddingup = 12;
    public float Y_max;
    public float Y_Min = 6.0f;

    public Vector3 FirstPosCam;
    public float MaxDistance = 5;
    public float temp_dis;
    public bool ready = false;
    private BuildPlatform platform;
    private float p;
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Humen").transform;
        FirstPosCam = transform.position;
        platform = FindObjectOfType<BuildPlatform>();
        Y_max = (platform.Levels[platform.CurrentLevel].Height * 100) - Paddingup;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            if (target == null)
            {

                if (GameObject.FindGameObjectWithTag("Body"))
                {
                    target = GameObject.FindGameObjectWithTag("Body").transform;
                }
                else
                {
                    return;
                }
            }
            if (target != null)
            {
                temp_dis = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(target.position.x, target.position.y, 0));
                if (target.position.y > Y_Min)
                {
                    transform.DOMove(new Vector3(transform.position.x, Mathf.Clamp(target.position.y, transform.position.y, Y_max), transform.position.z), SpeedFollow);

                }

                else
                {
                    transform.DOMove(FirstPosCam, 0.5f);
                }
            }
        }
    }

}
