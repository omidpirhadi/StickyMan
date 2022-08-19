using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using Diaco.Cannonman.UI;
using UnityEngine;
using UnityEngine.Profiling;

using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public LeaderboardData leaderboard;
    public Transform Camera;
    public BuildPlatform buildPlatform;
    public Controller controller;
    public Gun gun;
    
    public float RegenrationWorldInHeight = 950.0f;
    public Material SkyBox;
    
    public List<GameObject> HumenPrefabs;
    public ParticleSystem Effect;


    #region UI
    public StartMenu StartMenu_UI;
    public GameHUD GameHUD_UI;
    public EndGameMenu EndGameMenu_UI;

    public DialogNewAchive DialogNewAchive_UI;
    #endregion
    #region Property

    public GameObject humen_spwaned { set; get; }
    public float TotalScore { set; get; }
    public float BodyCurrentHeight { set; get; }
    public int CoinCount { set; get; }
    #endregion

    public bool OnUI = false;
         



    void Awake()
    {
        // GameAnalytics.Initialize();
        leaderboard = new LeaderboardData();
        leaderboard = LoadLeaderboard("cannonman");
        TotalScore = leaderboard.totalScore;
    }




    void LateUpdate()
    {
        if (humen_spwaned != null)
        {
            if (humen_spwaned.transform.position.y > BodyCurrentHeight)
                BodyCurrentHeight = humen_spwaned.transform.position.y;
        }
        if (Camera.transform.position.y > RegenrationWorldInHeight)
        {
            StartCoroutine(buildPlatform.ShiftEnvirment());
            RegenrationWorldInHeight += 1000;
            Debug.Log("Regenration World");
        }
        AchivmentHeight();
     
    }

    public void CompeletRecord ()
    {


        // GA_LevelFailEvent(2);
        GameHUD_UI.Show(false);
        EndGameMenu_UI.Show(true);

        controller.GameStarted = false;
        gun.automove = false;
        Camera.transform.position = new Vector3(0, 14f, -10);
        Camera.GetComponent<CameraFollow>().ready = false;
        Camera.GetComponent<CameraFollow>().target = null;

       // SaveLeaderboard("cannonman", leaderboard);

    }

    public float CalculateScore()
    {
        float coin_point = CoinCount * 100;
        float every_1_meter = Mathf.RoundToInt(BodyCurrentHeight * 10.0f);
        float every_100_meter = Mathf.RoundToInt(BodyCurrentHeight / 100.0f) * 1000;

        float total_score = Mathf.RoundToInt(coin_point + every_1_meter + every_100_meter);



        this.TotalScore += total_score;
        this.leaderboard.totalScore = this.TotalScore;

        if (this.BodyCurrentHeight > this.leaderboard.lastHeightRecord)
            this.leaderboard.lastHeightRecord = this.BodyCurrentHeight;
        return total_score;
    }
    private void GA_LevelCompeletEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "CompeletedLevel", level);

    }
    private void GA_LevelFailEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "FailLevel", level);

    }


    public IEnumerator LevelSpwan()
    {
        BodyCurrentHeight = 0;
        CoinCount = 0;
        buildPlatform.Resetvalue();
        if (buildPlatform.Envirement != null)
        {
            Destroy(buildPlatform.Envirement);
            buildPlatform.Envirement = null;
        }
        if (humen_spwaned)
        {
            Destroy(humen_spwaned);
            humen_spwaned = null;
        }

        RegenrationWorldInHeight = 950f;
        Camera.transform.position = new Vector3(0, 14f, -10);

        yield return new WaitForSecondsRealtime(00.1f);

        StartCoroutine(buildPlatform.CreateWalls());
        StartCoroutine(buildPlatform.CreatePlatform());
        StartCoroutine(buildPlatform.SpwanItems());

        yield return new WaitForSecondsRealtime(00.1f);

        StartMenu_UI.Show(false);
        EndGameMenu_UI.Show(false);
        GameHUD_UI.Show(true);

        controller.GameStarted = true;
        gun.GunReady();

        Camera.GetComponent<CameraFollow>().ready = true;
    }
   
    private void AchivmentHeight()
    {
        if(BodyCurrentHeight> leaderboard.AchiveHeight+100)
        {
            DialogNewAchive_UI.SetContext((leaderboard.AchiveHeight + 100).ToString());
            DialogNewAchive_UI.Show(true);
            leaderboard.AchiveHeight += 100;
            SaveLeaderboard("cannonman");
        }
    }

    #region Read and Write File
    public bool ExistTokenFile(string FileName)
    {
        bool find = false;
        if (File.Exists(Application.persistentDataPath + "//" + FileName + ".json"))
        {
            find = true;
        }
        return find;
    }
    public void SaveLeaderboard(string FileName)
    {

        var json_data = JsonUtility.ToJson(leaderboard);
        if (File.Exists(Application.persistentDataPath + "//" + FileName + ".json"))
        {
            File.Delete(Application.persistentDataPath + "//" + FileName + ".json");
        }
        File.WriteAllText(Application.persistentDataPath + "//" + FileName + ".json", json_data);
        Debug.Log("Data Saved");
    }
    public LeaderboardData LoadLeaderboard(string FileName)
    {
        LeaderboardData data = new  LeaderboardData();
        if (File.Exists(Application.persistentDataPath + "//" + FileName + ".json"))
        {
            var j_data = File.ReadAllText(Application.persistentDataPath + "//" + FileName + ".json");
            data = JsonUtility.FromJson<LeaderboardData>(j_data);

        }
        Debug.Log("Data Loaded");
        return data;
    }
    public void DeleteToken(string FileName)
    {

        if (File.Exists(Application.persistentDataPath + "//" + FileName + ".json"))
        {
            File.Delete(Application.persistentDataPath + "//" + FileName + ".json");
        }

    }


    
    #endregion

    #region Evenets


    private Action<bool> itemrestore;

    public event Action<bool> OnItemRestore
    {
        add { itemrestore += value; }
        remove { itemrestore -= value; }
    }
    protected void Handler_OnItemRestore(bool restore)

    {
        if (itemrestore != null)
        {
            itemrestore(restore);

        }
    }
    private Action itemdestory;

    public event Action OnItemDestory
    {
        add { itemdestory += value; }
        remove { itemdestory -= value; }
    }
    protected void Handler_OnItemDestroy()

    {
        if (itemdestory != null)
        {
            itemdestory();

        }
    }
    #endregion
}
[Serializable]
public struct LeaderboardData
{
    public float totalScore;
    public float lastHeightRecord;
    public int AchiveHeight;
}