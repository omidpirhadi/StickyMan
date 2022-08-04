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
    public float Y_DIFF_MAX = 5;
    public float SensivityMoveGun = 0.1f;
    public float MinYCamera;
   [SerializeField] private GameObject humen;
    private Vector3 firstTouch;
    private Vector3 secondTouch;

    private Vector3 current_touch , last_touch , touch_pos_after_diff;
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
                firstTouch = Camera.main.ScreenToWorldPoint(touch.position);
                firstTouch.z = 0;

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                secondTouch = Camera.main.ScreenToWorldPoint(touch.position);
                secondTouch.z = 0;
                current_touch = secondTouch;





                var y_diff = secondTouch.y - firstTouch.y;
               // float x_change = (current_touch - last_touch).x;
               // float y_change = (current_touch - last_touch).y;
                if (y_diff >= Y_DIFF_MAX)
                {
                   
                    var range = Vector3.Distance(firstTouch, secondTouch);
                    ring.SetPosition(Gun.transform.position);
                    ring.SetRange(range-Y_DIFF_MAX);
                    ring.SetRotation(secondTouch);
                    
                    Gun.RotateGunToAim(secondTouch);
                }
                else
                {
                    Gun.ChangePosition(current_touch, last_touch, SensivityMoveGun);
                }
                last_touch = current_touch;
            }
            else if (touch.phase == TouchPhase.Ended)
            {

                var f = ring.CalculateDirectionForce();
                if (ring.PowerRange > 0)
                    Gun.Fire(f, ring.PowerRange);
                ring.Reset();
              //  selecetdGun = false;
                // Gun = null;
                Time.timeScale = 1.0f;

            }
        }

    }
 
    private void SpwanHumen()
    {
       humen = Instantiate(HumenPrefabs[0], SpwanPlaceHume.transform.position, Quaternion.identity);
    }
}
