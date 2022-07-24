using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Controller : MonoBehaviour
{

    public Gun Gun;
    public RingPower ring;
    public List< GameObject> HumenPrefabs;

    public Transform SpwanPlaceHume;
    public LayerMask GunMask;

    public float MinYCamera;
   [SerializeField] private GameObject humen;
    private Vector3 firstTouch;
    private Vector3 secondTouch;

    private Ray ray;
    private RaycastHit hit;
    private bool selecetdGun;
    void Start()
    {
       // ring = GetComponent<RingPower>();
    }

    
    void Update()
    {
       /* if (humen.transform.position.y < MinYCamera)
        {
            Camera.main.GetComponent<PositionConstraint>().constraintActive = false;
        }
        else
        {
            Camera.main.GetComponent<PositionConstraint>().constraintActive = true;
        }*/
        Touch2();
    }
    /* public Transform point;
     private Vector3 PointClick;
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

     }*/
    private void Touch2()
    {
        if (Input.touchCount > 0)
        {


            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                selecetdGun = false;
                ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, 10, GunMask))
                {
                    if (hit.collider)
                    {
                        var temp_pos = hit.transform.position;
                        temp_pos.z = 0;
                        firstTouch = temp_pos;
                        ring.SetPosition(temp_pos);
                        this.Gun = hit.transform.GetComponentInParent<Gun>();
                        selecetdGun = true;
                        Time.timeScale = 0.2f;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (selecetdGun)
                {
                    secondTouch = Camera.main.ScreenToWorldPoint(touch.position);
                    secondTouch.z = 0;
                    var range = Vector3.Distance(firstTouch, secondTouch);
                    ring.SetRange(range);
                    ring.SetRotation(secondTouch);
                    RotateGunToAim(secondTouch);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (selecetdGun)
                {
                    var f = ring.CalculateDirectionForce();
                    //  Debug.Log(f);
                    Gun.Fire(f, ring.PowerRange);
                    ring.Reset();
                    selecetdGun = false;
                    Gun = null;
                    Time.timeScale = 1.0f;
                }
            }
        }

    }
    private void RotateGunToAim( Vector3 point)
    {

        var dir = (point - Gun.transform.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
    private void SpwanHumen()
    {
       humen = Instantiate(HumenPrefabs[0], SpwanPlaceHume.transform.position, Quaternion.identity);
    }
}
