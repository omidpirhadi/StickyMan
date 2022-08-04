using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private new Rigidbody rigidbody;

    private SettingUI settingUI;
    public void Start()
    {
        settingUI = FindObjectOfType<SettingUI>();
        settingUI.OnChangeSetting += OnChangeSetting;
        Ammo(0);
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

            b.AddForce(force.normalized * (power * PowerFire), forceMode);
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
    public void ChangePosition(Vector3 current, Vector3 last ,float sensivity)
    {
    

        var pos = this.transform.localPosition;
        var dir = (last - current).normalized;
        pos.x += -dir.x * sensivity;
        this.transform.localPosition = new Vector3(Mathf.Clamp(pos.x, -5, 5), pos.y, pos.z);

    }
}
