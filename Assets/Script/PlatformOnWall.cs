using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlatformOnWall : MonoBehaviour
{
    public bool IsMovable = false;
   // public float MoveUpAndDown;
    public Vector3 MoveLeft, MoveRight;
    public LoopType loopType;
    public Ease ease;
    private Transform cam;
   

    private Transform parent;

    public void Start()
    {
        if(IsMovable)
        {
            MoveLeft.y = transform.position.y;
            MoveLeft.z = transform.position.z;
            MoveRight.y = transform.position.y;
            MoveRight.z = transform.position.z;
            MoveLeftAndRight();
        }
       
        cam = Camera.main.transform;
    
       
    }
    // Update is called once per frame
    public void LateUpdate()
    {
        var temp_y = cam.transform.position.y - this.transform.position.y;
        if (temp_y > 50)
        {

            //Debug.Log("Des" + gameObject.name);
          

                Destroy(this.gameObject);
            
 
        }
    }

    public void MoveLeftAndRight()
    {
        transform.DOMove(MoveLeft, 3).OnComplete(() =>
        {

            transform.DOMove(MoveRight, 3).OnComplete(() => {

                MoveLeftAndRight();

            }).SetEase(ease).SetLoops(1, loopType); 
        }).SetEase(ease).SetLoops(1, loopType);
    }

}
