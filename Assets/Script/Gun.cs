using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
public class Gun : MonoBehaviour
{
    public float MaxCapacityAmmo = 100;
    public float AmmoCount = 50;
    public Image CapasityGun_image;
    public Text CapacityGun_text;
    public Rigidbody Bullet;
    public Transform BulletPlace;
    public float PowerFire;
    public ForceMode forceMode;
    public float SpeedChangePosition = 0.5f;
    private new Rigidbody rigidbody;
    private Tween autoposition;
    private SettingUI settingUI;
    private int dir = -1;
    public bool automove = false;
   // public bool IsGunReady = false;
    public void Start()
    {
        settingUI = FindObjectOfType<SettingUI>();
        settingUI.OnChangeSetting += OnChangeSetting;
     //   AutoChangePosition();
       
    }
    public void Update()
    {

            if (automove == true)
            {
                if (dir == -1)
                {
                    transform.Translate(Vector3.left * Time.fixedUnscaledDeltaTime * SpeedChangePosition, Space.World);

                }
                else if (dir == 1)
                {
                    transform.Translate(Vector3.right * Time.fixedUnscaledDeltaTime * SpeedChangePosition, Space.World);
                }
                if (transform.position.x <= -4.49)
                    dir = 1;
                else if (transform.position.x >= 4.49)
                    dir = -1;
            }
        
    }
    private void OnChangeSetting()
    {
        
        PowerFire = settingUI.powergun;
    }

    public void Fire(Vector3 force, float power)
    {
        if (AmmoCount > 0)
        {
            var b = Instantiate(Bullet, BulletPlace.position, Quaternion.identity);
            var f = force.normalized * (power * PowerFire);
            f.z = 00.0f;
            b.AddForce(f, forceMode);
            Ammo(-1);
        }

    }
    public void Ammo(int a)
    {
        AmmoCount += a;
        if (AmmoCount > 0 && AmmoCount <= MaxCapacityAmmo)
        {
            float step = AmmoCount / MaxCapacityAmmo;
            CapasityGun_image.fillAmount = step;
            CapacityGun_text.text = "Ammo:" + AmmoCount.ToString();
        }
    }

   /* public void ChangePosition(Vector3 current, Vector3 last ,float sensivity)
    {
    

        var pos = this.transform.localPosition;
        var dir = (last - current).normalized;
        pos.x += -dir.x * sensivity;
        this.transform.localPosition = new Vector3(Mathf.Clamp(pos.x, -5, 5), pos.y, pos.z);

    }*/
   /* public void ChangePositionWithSlider(float x, float sensivity)
    {


        var pos = this.transform.localPosition;
       
        pos.x = x * sensivity;
        this.transform.localPosition = new Vector3(Mathf.Clamp(pos.x, -5, 5), pos.y, pos.z);

    }*/
    
   /* public void AutoChangePosition()
    {
       autoposition =  this.transform.DOMoveX(-5, SpeedChangePosition).SetEase(Ease.Linear).OnComplete(() => {
            this.transform.DOMoveX(+5, SpeedChangePosition).SetEase(Ease.Linear).OnComplete(() => { AutoChangePosition(); });
        });
        
        
    }*/
    [Button("KillAutoMove",ButtonSizes.Medium)]
    public void  AutoMoveKill()

    {
        // autoposition.Kill();
        automove = false;
    //    Debug.Log("AutoPlayKill");
    }
    [Button("PlayAutoMove", ButtonSizes.Medium)]
    public void AutoMovePlay()

    {
        automove = true;
       // AutoChangePosition();
     //   Debug.Log("AutoPlayRun");
    }
    public void RotateGunToAim(Vector3 point)
    {

        var dir = (point - this.transform.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
    [Button("GunReady", ButtonSizes.Medium)]
    public void GunReady()
    {

        Ammo(50);
        transform.localPosition = new Vector3(0f, -11.91f, 10);
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        dir = -1;
        AutoMovePlay();
       // IsGunReady = true;
    }
}
