using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public bool GunM24 = false;
    public Diaco.Cannonman.UI.GameHUD GameHUD_UI;
    public GameObject HumenPrefabs;
    public ParticleSystem CannonShotEffect;
    public ParticleSystem M24ShotEffect;
    public float MaxCapacityAmmo = 100;
    public float AmmoCount = 50;
  ///  public Image CapasityGun_image;
  //  public Text CapacityGun_text;
    public Rigidbody Bullet_prefab;
    public Transform BulletPlace;
    public Transform BulletPlace2;
    public float PowerFire;
    public ForceMode forceMode;
    public float SpeedChangePosition = 0.5f;
   /// private new Rigidbody rigidbody;
  //  private Tween autoposition;
  //  private SettingUI settingUI;
    private int dir = -1;
    public bool automove = false;
    private AudioSource audioSource;
    private GameManager gameManager;
    private bool firstShot = true;
   // public bool IsGunReady = false;
    public void Start()
    {
      ////  settingUI = FindObjectOfType<SettingUI>();
      ///  settingUI.OnChangeSetting += OnChangeSetting;
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
       // GameHUD_UI = FindObjectOfType<Diaco.Cannonman.UI.GameHUD>();
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
   /* private void OnChangeSetting()
    {
        
        PowerFire = settingUI.powergun;
    }*/

    public void Fire(Vector3 force, float power)
    {
        var f = force.normalized * (power * PowerFire);
        f.z = 00.0f;
        if (firstShot == false)
        {
            if (AmmoCount > 0)
            {
 
           
                    CannonShotEffect.Play(true);
                    audioSource.Play();
                    var b = Instantiate(Bullet_prefab, BulletPlace.position, Quaternion.identity);

                    b.AddForce(f, forceMode);

                
            

                Ammo(-1);
            } 

        }
        else
        {
            HumenShoot(f);
           
        }
    }
    public void Ammo(int a)
    {
        
        if (AmmoCount >= 0 && AmmoCount <= MaxCapacityAmmo)
        {
            AmmoCount += a;
            GameHUD_UI.Ammo_txt.text = AmmoCount.ToString();
        }
    }


    
    public void  AutoMoveKill()

    {
        // autoposition.Kill();
        automove = false;
    //    Debug.Log("AutoPlayKill");
    }
   
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
 
    public void GunReady()
    {

        AmmoCount = 0;
        transform.localPosition = new Vector3(0f, -14.0f, 10);
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        dir = -1;
        AutoMovePlay();
        Ammo(20);
        firstShot = true;
       // IsGunReady = true;
    }
    private void HumenShoot(Vector3 force)
    {
       gameManager.humen_spwaned = Instantiate(HumenPrefabs, BulletPlace.position, HumenPrefabs.transform.rotation);
       gameManager.humen_spwaned.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        firstShot = false;

    }
}
