using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public InputField MassBody;
    public InputField PowerRange;
    public InputField PowerGun;
    public InputField BounceWall;
    public InputField BounceBody;
    public InputField BounceBullet;

    public Button Set_button;
    public Button OpenSetting;
    public Button Play_btn;
    public Button Pause_btn;
    public Button Reset_btn;
    public Button CloseSetting;

    public float massbody;
    public float powerrange;
    public float powergun;
    

    public PhysicMaterial wall, body, bullet;

    public GameObject PanelSetting;
    void Start()
    {
        Set_button.onClick.AddListener(() => {

            if (MassBody.text.Length > 0)
                massbody = Convert.ToSingle(MassBody.text);
            else
                massbody = 1;

            if (PowerRange.text.Length > 0)
                powerrange = Convert.ToSingle(PowerRange.text);
            else
                powerrange = 5;

            if (PowerGun.text.Length > 0)
                powergun = Convert.ToSingle(PowerGun.text);
            else
                powergun = 50;

            if (BounceWall.text.Length > 0)
                wall.bounciness = Convert.ToSingle(BounceWall.text);
            else
                wall.bounciness = 0.2f;

            if (BounceBody.text.Length > 0)
                body.bounciness = Convert.ToSingle(BounceBody.text);
            else
                body.bounciness = 0.2f;

            if (BounceBullet.text.Length > 0)
                bullet.bounciness = Convert.ToSingle(BounceBullet.text);
            else
               bullet.bounciness = 1;
           
            Handler_Change();
        });
        OpenSetting.onClick.AddListener(() => {
            PanelSetting.SetActive(true);
        });
        CloseSetting.onClick.AddListener(() => {
            PanelSetting.SetActive(false);
        });
        
        Play_btn.onClick.AddListener(() => {
            Time.timeScale = 1;
        });
        Pause_btn.onClick.AddListener(() => {
            Time.timeScale = 0;
        });
        Reset_btn.onClick.AddListener(() => {
            Time.timeScale = 0;
            Camera.main.transform.position = new Vector3(0.0f,6.3f,-10.0f);
           var body =  GameObject.FindGameObjectWithTag("Body");
            body.GetComponent<Rigidbody>().velocity = Vector3.zero;
            body.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            body.transform.position = new Vector3(0, 16, 0);
            body.transform.eulerAngles = new Vector3(0, 180, 0);

        });
    }



    private Action change_setting;
    public event Action OnChangeSetting
    {
        add { change_setting += value; }
        remove { change_setting -= value; }
    }
    protected void Handler_Change()
    {
        if(change_setting != null)
        {
            change_setting();
        }
    }

}
