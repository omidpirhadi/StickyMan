using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    public Transform Cam;
    public Vector3 current_pos, last_pos, deltapos;
    public float prallexValue;
    private SpriteRenderer Sprite;
    private float orthoSize;
    void Start()
    {
          Cam = Camera.main.transform;
          current_pos = Cam.position;
          last_pos = Cam.position;
     
    }

    // Update is called once per frame
    void Update()
    {
      
      current_pos = Cam.position;
       deltapos = last_pos - current_pos;
       this.transform.position += deltapos * prallexValue;
       last_pos = current_pos;
    }
}
