using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public Gun Gun;
    public Transform point;
    private Vector3 PointClick;
    void Start()
    {
        
    }

    
    void Update()
    {
        Touch();
    }
    private void Touch()
    {
        if (Input.touchCount > 0)
        {


            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                PointClick = Camera.main.ScreenToWorldPoint(touch.position);
                PointClick.z = 0;
                point.transform.position = PointClick;
                AimGun(PointClick);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Gun.Fire();
            }
        }

    }
    private void AimGun( Vector3 point)
    {

        var dir = (this.point.position - Gun.transform.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
}
