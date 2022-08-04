using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using DG.Tweening;
public class Controller : MonoBehaviour
{

    public Gun Gun;
    public RingPower ring;
    public List< GameObject> HumenPrefabs;

    public Transform SpwanPlaceHume;
    public LayerMask GunMask;

    public Slider PosSlider; 
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
    private bool run = false;
    void Start()
    {
        PosSlider.onValueChanged.AddListener((x) => {

            Gun.ChangePositionWithSlider(x, SensivityMoveGun);

        });
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
                Time.timeScale = 0.2f;
                firstTouch = Camera.main.ScreenToWorldPoint(touch.position);
                firstTouch.z = 0;
                current_touch = firstTouch;
                Gun.AutoMoveKill();
                ring.PlaySetRangWithTime();
                ring.SetPosition(Gun.transform.position);
                ring.SetRotation(firstTouch);
                Gun.RotateGunToAim(firstTouch);
            }
            else if (touch.phase == TouchPhase.Moved)
            {

                secondTouch = Camera.main.ScreenToWorldPoint(touch.position);
                secondTouch.z = 0;
                current_touch = secondTouch;
                /*if (run == false)
                {
                    ring.SetRangeWithTime();
                    run = true;
                }*/

               
                ring.SetRotation(secondTouch);

                Gun.RotateGunToAim(secondTouch);



                last_touch = current_touch;

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                run = false;
                Time.timeScale = 1.0f;
                Gun.AutoMovePlay();
                ring.KillSetRangWithTime();
                var f = ring.CalculateDirectionForce();
                if (ring.PowerRange > 0)
                    Gun.Fire(f, ring.PowerRange);
                ring.Reset();
                //  selecetdGun = false;
                // Gun = null;
                

            }
        }

    }
 
    private void SpwanHumen()
    {
       humen = Instantiate(HumenPrefabs[0], SpwanPlaceHume.transform.position, Quaternion.identity);
    }
}
