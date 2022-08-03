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
    public float SensivityMoveGun = 0.1f;
    public float MinYCamera;
   [SerializeField] private GameObject humen;
    private Vector3 firstTouch;
    private Vector3 secondTouch;

    private Vector3 current_touch , last_touch;
    private Ray ray;
    private RaycastHit hit;
    private bool selecetdGun;
    void Start()
    {
      
    }

    
    void Update()
    {

        Touch2();
    }

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
                      //  this.Gun = hit.transform.GetComponentInParent<Gun>();
                        selecetdGun = true;
                        Time.timeScale = 0.2f;
                    }
                }
                else
                {
                    firstTouch = Camera.main.ScreenToWorldPoint(touch.position);
                    firstTouch.z = 0;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                secondTouch = Camera.main.ScreenToWorldPoint(touch.position);
                secondTouch.z = 0;
                current_touch = secondTouch;

                if (selecetdGun)
                {


                    var range = Vector3.Distance(firstTouch, secondTouch);
                    ring.SetRange(range);
                    ring.SetRotation(secondTouch);
                    RotateGunToAim(secondTouch);
                }
                else
                {
                    Gun.ChangePosition(current_touch, last_touch, SensivityMoveGun);

                }
                last_touch = current_touch;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (selecetdGun)
                {
                    var f = ring.CalculateDirectionForce();

                    Gun.Fire(f, ring.PowerRange);
                    ring.Reset();
                    selecetdGun = false;
                   // Gun = null;
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
