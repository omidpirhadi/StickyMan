using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
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
    }

    private void OnChangeSetting()
    {
        PowerFire = settingUI.powergun;
    }

    public void Fire(Vector3 force , float power)
    {
        
        var b = Instantiate(Bullet, BulletPlace.position, Quaternion.identity);

        b.AddForce(force.normalized * (power * PowerFire), forceMode);
        
    }
}
