using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class BuildPlatform : MonoBehaviour
{
    //  public Button sss;
    public int CurrentLevel;
    public List<LEVEL> Levels;
    public GameObject Envirement;
    public GameManager gameManager;
    


    private List<GameObject> platformsSpawned = new List<GameObject>();


    private GameObject platform_spawned;
    private int platformShit = 0;
    private int lastPlatform_index = 0;
    private int item_shift = 0;
    private int EnvirmentShiftCount = 0;


    public IEnumerator CreateWalls()
    {

        Envirement = new GameObject("Envirment");
        platformsSpawned = new List<GameObject>();
        // WallsSpawned = new List<GameObject>();
        int dis = Levels[CurrentLevel].DistansWallEcheOther;
        var wall_prefabs = Levels[CurrentLevel].Wall_prefab;
        var endline_prefab = Levels[CurrentLevel].EndLine_prefab;
        int h = Levels[CurrentLevel].Height;
        Vector3 pos_left;
        Vector3 pos_right;
        for (int i = 0; i < h; i++)
        {
            if (i == 0)
            {
                pos_left = new Vector3(0 - dis, 0, 0);
                pos_right = new Vector3(0 + dis, 0, 0);

            }
            else
            {
                pos_left = new Vector3(0 - dis, 100 * i, 0);
                pos_right = new Vector3(0 + dis, 100 * i, 0);

            }
            /*  if (i == h - 1)
              {
                  var endline = Instantiate(endline_prefab, new Vector3(0, (100 * i) + 100, 0), Quaternion.identity, Envirement.transform);
              }*/


            var wall_L = Instantiate(wall_prefabs, pos_left, Quaternion.identity, Envirement.transform);
            wall_L.name = "wall_left" + i;

            var wall_R = Instantiate(wall_prefabs, pos_right, Quaternion.identity, Envirement.transform);
            wall_R.name = "wall_right" + i;

            //   WallsSpawned.Add(wall_L.gameObject);
            // WallsSpawned.Add(wall_R.gameObject);

            //CreatePlatform(envirment.transform);
        }
        MaileStoneSpwan(0 + (1000 * EnvirmentShiftCount), 1000 + (1000 * EnvirmentShiftCount));
        MaileStoneLastClimbSpwan(gameManager.leaderboard.lastHeightRecord);
        EnvirmentShiftCount++;
        yield return null;

    }

    public IEnumerator CreatePlatform()
    {


        Vector3 pos_platform = new Vector3();
        float temp_platform_distance_y = 0;
        int h = (Levels[CurrentLevel].Height * 100) + (1000 * platformShit);
        //  Debug.Log("H:" + h);
        float padding_down = Levels[CurrentLevel].PadingDown;
        float padding_up = Levels[CurrentLevel].PadingUp;
        //   Debug.Log($"Padup{padding_up}Paddown{padding_down}");
        float distance_x = Levels[CurrentLevel].DistansWallEcheOther;
        float distance_y = Levels[CurrentLevel].DistancePlatformEachOther;

        int n = Mathf.RoundToInt((h - (1000 * platformShit) - (padding_up + padding_down)) / distance_y);
        ///   Debug.Log("T:" + (h - (padding_down + padding_up)));

        //   Debug.Log("N:" + n);


        for (int i = 0; i < n; i++)
        {
            int rand_pos = UnityEngine.Random.Range(0, 3);

            pos_platform = new Vector3(0.0f, (padding_down) + (1000 * platformShit), 0.0f);
            //  Debug.Log("Y:" + pos_platform.y);
            pos_platform.y = Mathf.Clamp(pos_platform.y + (temp_platform_distance_y), (padding_down) + (1000 * platformShit), (h - padding_up) + (1000 * platformShit));
            // Debug.Log("Y2:" + pos_platform.y);
            if (pos_platform.y <= (h - padding_up) + (1000 * platformShit))
            {
                if (rand_pos == 0)
                {
                    pos_platform = new Vector3((distance_x - Levels[CurrentLevel].Offset_X) * -1, pos_platform.y, 0.0f);

                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].LeftPlatform.Count);

                    var platform = Levels[CurrentLevel].LeftPlatform[rand_platform].platform_prefab;
                    platform_spawned = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, Envirement.transform);
                    platform_spawned.transform.localScale = Levels[CurrentLevel].LeftPlatform[rand_platform].Scale;
                }
                else if (rand_pos == 1)
                {
                    pos_platform = new Vector3(0, pos_platform.y, 0.0f);
                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].MedillePlatform.Count);

                    var platform = Levels[CurrentLevel].MedillePlatform[rand_platform].platform_prefab;
                    platform_spawned = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, Envirement.transform);
                    platform_spawned.transform.localScale = Levels[CurrentLevel].MedillePlatform[rand_platform].Scale;
                }
                else if (rand_pos == 2)
                {
                    pos_platform = new Vector3((distance_x - Levels[CurrentLevel].Offset_X) * 1, pos_platform.y, 0.0f);
                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].RightPlatform.Count);

                    var platform = Levels[CurrentLevel].RightPlatform[rand_platform].platform_prefab;
                    platform_spawned = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, Envirement.transform);
                    platform_spawned.transform.localScale = Levels[CurrentLevel].RightPlatform[rand_platform].Scale;
                }


                temp_platform_distance_y += distance_y;
            }

            platformsSpawned.Add(platform_spawned);
            ///Debug.Log("jjjjjjjjjjjjj" + i);

        }
        platformShit++;
        // lastPlatform_index = platformsSpawned.Count - 1;
        // Debug.Log("plast" + (platformsSpawned.Count - 1));

        yield break;
    }



    public IEnumerator SpwanItems()
    {
        //  Debug.Log("DASDASD");
        float offset_y = 0;
        int index = 0;
        if (item_shift == 0)
        {
            index = 0;
            lastPlatform_index = platformsSpawned.Count - 1;
            //  Debug.Log("index1"+index);
        }
        else
        {
            index = lastPlatform_index;
            lastPlatform_index = platformsSpawned.Count - 1;
            //  Debug.Log("index2" + index);
        }
        for (int i = index; i < platformsSpawned.Count; i++)
        {

            int rand_chance_item = UnityEngine.Random.Range(0, 2); // 0 = ammo_box,  1 = coin_box//
            var pos = platformsSpawned[i].transform.position;
            Vector3 pos_item = new Vector3();
            if (rand_chance_item == 0)
            {

                if (pos.x > 0)// right
                {

                    pos_item.x -= Levels[CurrentLevel].DistanceItemFromPlatform.x;
                    pos_item.y += Levels[CurrentLevel].DistanceItemFromPlatform.y;
                }
                else if (pos.x < 0) // left
                {
                    pos_item.x += Levels[CurrentLevel].DistanceItemFromPlatform.x;
                    pos_item.y += Levels[CurrentLevel].DistanceItemFromPlatform.y;
                }
                else if (pos.x == 0)// middle
                {
                    pos_item.x = 0;
                    pos_item.y += Levels[CurrentLevel].DistanceItemFromPlatform.y;
                }
                SpwanBoxAmmo(pos + pos_item);
            }
            else
            {
                var coin_count = (Levels[CurrentLevel].DistancePlatformEachOther - 10) / Levels[CurrentLevel].CoinDistanceEachOther;

                pos_item = pos;
                if (pos.x > 0)// right
                {

                    pos_item.x -= Levels[CurrentLevel].DistanceItemFromPlatform.x;
                    pos_item.y += Levels[CurrentLevel].DistanceItemFromPlatform.y;
                }
                else if (pos.x < 0) // left
                {
                    pos_item.x += Levels[CurrentLevel].DistanceItemFromPlatform.x;
                    pos_item.y += Levels[CurrentLevel].DistanceItemFromPlatform.y;
                }
                else if (pos.x == 0)// middle
                {
                    pos_item.x = 0;
                    pos_item.y += Levels[CurrentLevel].DistanceItemFromPlatform.y;
                }
                offset_y = pos_item.y;

                for (int j = 0; j < coin_count; j++)
                {
                    SpwanCoin(new Vector3(pos_item.x, offset_y, pos_item.z));
                    offset_y += 2.5f;


                }

            }
            yield return null;

            //  Debug.Log("ITEM");

        }

        item_shift++;
    }
    private void SpwanBoxAmmo(Vector3 pos)
    {

        Instantiate(Levels[CurrentLevel].AmmoBox_prefab, pos, Levels[CurrentLevel].AmmoBox_prefab.transform.rotation, Envirement.transform);

    }
    private void SpwanCoin(Vector3 pos)
    {
        Instantiate(Levels[CurrentLevel].CoinBox_prefab, pos, Levels[CurrentLevel].CoinBox_prefab.transform.rotation, Envirement.transform);
    }

    private void MaileStoneSpwan(int min, int max)
    {
        for (int i = min; i <= max; i++)
        {

            if (i % 100 == 0)
            {
                var mailstone = Instantiate(Levels[CurrentLevel].MaileStone_Prefab, new Vector3(0, i, 0), Quaternion.identity, Envirement.transform);
                mailstone.Set_MailStone(i + "M", "w");
            }

        }
    }
    private void MaileStoneLastClimbSpwan(float y)
    {
        if (y > 100.0f)
        {
            var mailstone = Instantiate(Levels[CurrentLevel].MaileStoneLastClimb_Prefab, new Vector3(0, y, 0), Quaternion.identity, Envirement.transform);
        }

    }
    public IEnumerator ShiftEnvirment()
    {
        // ShitWallToUP();
        StartCoroutine(CreatePlatform());
        StartCoroutine(SpwanItems());
        MaileStoneSpwan(0 + (1000 * EnvirmentShiftCount), 1000 + (1000 * EnvirmentShiftCount));
        EnvirmentShiftCount++;
        yield return null;
    }

    public void Resetvalue()
    {
        platformShit = 0;
        lastPlatform_index = 0;
        item_shift = 0;
        EnvirmentShiftCount = 0;
    }
}

[Serializable]
public struct LEVEL
{
    
    [BoxGroup("Wall Settings")]
    public int Height;
    [BoxGroup("Wall Settings")]
    public int DistansWallEcheOther; 
    [BoxGroup("Wall Settings")]
    public Wall Wall_prefab;
    [BoxGroup("Plastform Settings")]
    public float DistancePlatformEachOther; 
    [BoxGroup("Plastform Settings/Padding")]
    public float PadingDown;  
    [BoxGroup("Plastform Settings/Padding")]
    public float PadingUp;
    [BoxGroup("Plastform Settings")]
    public float Offset_X;
  //  [BoxGroup("Plastform Settings")]
    //public Vector3 PlatformScale;
    [BoxGroup("Plastform Settings")]
    public List<Platform> LeftPlatform;
    [BoxGroup("Plastform Settings")]
    public List<Platform> MedillePlatform;
    [BoxGroup("Plastform Settings")]
    public List<Platform> RightPlatform;

    [BoxGroup("Items Settings")]
    public Vector3 DistanceItemFromPlatform;
    [BoxGroup("Items Settings")]
    public GameObject AmmoBox_prefab;
    [BoxGroup("Items Settings")]
    public GameObject CoinBox_prefab;
    [BoxGroup("Items Settings")]
    public int RepeatCoinBox;
    [BoxGroup("Items Settings")]
    public float CoinDistanceEachOther;
    [BoxGroup("Items Settings")]
    public GameObject EndLine_prefab;
    [BoxGroup("Items Settings")]
    public MailStone MaileStone_Prefab;
    [BoxGroup("Items Settings")]
    public MailStone MaileStoneLastClimb_Prefab;
}
[Serializable]
public struct Platform
{

    public GameObject platform_prefab;
    public Vector3 Scale;
    public bool UseInEnvirment;
}


