﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameAnalyticsSDK;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public BuildPlatform buildPlatform;
    public Controller controller;
    
    public Gun gun;
    public Transform Camera;
    public Material SkyBox;
    public Transform SpwanPlaceHume;
    public List<GameObject> HumenPrefabs;
    public ParticleSystem Effect;

    public int CurrentLevel = 0;
    public float HeightLevel;
    public float BodyCurrentHeight;
    private int CoinCount;

    public TMPro.TMP_Text Coin_text;
    public TMPro.TMP_Text Ammo_text;
    public Slider HeightSlider;
    public GameObject StartMenu_ui;
    public Button Start_btn;

    public GameObject EndGameMenu_ui;
    public Button nextlevel_btn;
    public Button resetlevel_btn;
    public Button exitapp_btn;

    public GameObject gameoverbox;
    public GameObject winnerbox;

    public  GameObject humen_spwaned;
    void Awake()
    {
       // GameAnalytics.Initialize();

    }
    void Start()
    {
        Start_btn.onClick.AddListener(() =>
        {
            StartCoroutine(LevelSpwan());
        });
        nextlevel_btn.onClick.AddListener(() => {

            StartCoroutine(LevelSpwan());
        });

        resetlevel_btn.onClick.AddListener(() => {
            StartCoroutine(LevelReset());
        });
        exitapp_btn.onClick.AddListener(() => {

            Application.Quit();
        });
        SetCoinvalue(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {

        SetGridantSkyBox();
        SetSliderHeightHumen();
        SetAmmoTextInUI();
    }
    public void LevelCompeleted()
    {
       //// GA_LevelCompeletEvent(1);
        Effect.Play();
        EndGameMenu_ui.SetActive(true);
        gameoverbox.SetActive(false);
        winnerbox.SetActive(true);
        controller.GameStarted = false;
        gun.automove = false;
        Camera.transform.position = new Vector3(0, 13.28f, -10);

        
        Camera.GetComponent<CameraFollow>().ready = false;
        Camera.GetComponent<CameraFollow>().target = null;

    }
    public void LevelFail()
    {
       // GA_LevelFailEvent(2);
        EndGameMenu_ui.SetActive(true);
        gameoverbox.SetActive(true);
        winnerbox.SetActive(false);
        controller.GameStarted = false;
        gun.automove = false;
        Camera.transform.position = new Vector3(0, 13.28f, -10);
        Camera.GetComponent<CameraFollow>().ready = false;
        Camera.GetComponent<CameraFollow>().target = null;

    }

    private void GA_LevelCompeletEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "CompeletedLevel", level);

    }
    private void GA_LevelFailEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "FailLevel", level);

    }
    private void SpwanHumen()
    {
        Instantiate(HumenPrefabs[0], SpwanPlaceHume.transform.position, HumenPrefabs[0].transform.rotation);
    }
    private void HumenShoot()
    {
       humen_spwaned = Instantiate(HumenPrefabs[0], gun.BulletPlace.position, HumenPrefabs[0].transform.rotation);
        humen_spwaned.GetComponent<Rigidbody>().AddForce(Vector3.up * 100, ForceMode.Impulse);

    }
    private void SetGridantSkyBox()
    {
       if (humen_spwaned)
        {
            BodyCurrentHeight = humen_spwaned.transform.position.y;
            var amount = BodyCurrentHeight / HeightLevel;

            SkyBox.SetFloat("_mult", Mathf.Clamp(amount, 0, 1));
            SkyBox.SetFloat("_pwer", Mathf.Clamp(1 - amount, 0, 1));
        }
    }
    private void SetSliderHeightHumen()
    {
        if (humen_spwaned)
        {
            BodyCurrentHeight = humen_spwaned.transform.position.y;
            var amount = BodyCurrentHeight / HeightLevel;

            HeightSlider.value = amount;
        }
    }

    public void  SetCoinvalue(int c)
    {
        CoinCount += c;
        Coin_text.text = CoinCount.ToString();
      //  Debug.Log("1");
    }
    private void SetAmmoTextInUI()
    {
        Ammo_text.text = gun.AmmoCount.ToString();
    }
    private IEnumerator LevelSpwan()
    {
        Camera.transform.position = new Vector3(0, 13.28f, -10);
        Destroy(buildPlatform.Envirement);
        HeightSlider.value = 0;

       
        if (humen_spwaned)

        {
            Destroy(humen_spwaned);
            humen_spwaned = null;
        }
        yield return new WaitForSecondsRealtime(00.1f);
        buildPlatform.CreateEnvirment();
        yield return new WaitForSecondsRealtime(00.1f);
        buildPlatform.CreatePlatform();
        yield return new WaitForSecondsRealtime(00.1f);
        buildPlatform.SpwanItems();
        yield return new WaitForSecondsRealtime(00.1f);




        HeightSlider.gameObject.SetActive(true);
        StartMenu_ui.SetActive(false);
        EndGameMenu_ui.SetActive(false);
       
        
        yield return new WaitForSecondsRealtime(00.1f);

        controller.GameStarted = true;
        //HumenShoot();
        gun.GunReady();
        Camera.GetComponent<CameraFollow>().ready = true;


    }
    private IEnumerator LevelReset()
    {
        Camera.transform.position = new Vector3(0, 13.28f, -10);
        if (humen_spwaned)

        {
            Destroy(humen_spwaned);
            humen_spwaned = null;
        }

        HeightSlider.value = 0;

        yield return new WaitForSecondsRealtime(00.1f);




        yield return new WaitForSecondsRealtime(00.1f);
        controller.GameStarted = true;


        HeightSlider.gameObject.SetActive(true);
        StartMenu_ui.SetActive(false);
        EndGameMenu_ui.SetActive(false);
        //HumenShoot();
        gun.GunReady();
        Camera.GetComponent<CameraFollow>().ready = true;

    }
}
