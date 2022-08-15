using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
public class BuildPlatform : MonoBehaviour
{
    public int CurrentLevel;
    public List<LEVEL> Levels;
    public GameObject Envirement;
    private GameObject platform_spawned;

    private List<GameObject> platformsSpawned = new List<GameObject>();
    [Button("CreateEnvirment", ButtonSizes.Medium)]
    public IEnumerator CreateEnvirment()
    {

        Envirement = new GameObject("Envirment");
        platformsSpawned = new List<GameObject>();
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
            if (i == h - 1)
            {
                var endline = Instantiate(endline_prefab, new Vector3(0, (100 * i) + 100, 0), Quaternion.identity, Envirement.transform);
            }


            var wall_L = Instantiate(wall_prefabs, pos_left, Quaternion.identity, Envirement.transform);
            wall_L.name = "wall_left" + i;

            var wall_R = Instantiate(wall_prefabs, pos_right, Quaternion.identity, Envirement.transform);
            wall_R.name = "wall_right" + i;

            //CreatePlatform(envirment.transform);
        }

        yield return null;
        
    }

    [Button("CreatePlatform", ButtonSizes.Medium)]
    public IEnumerator CreatePlatform()
    {

        bool end = false;
        Vector3 pos_platform = new Vector3();

        int h = Levels[CurrentLevel].Height * 100;
        float distance = Levels[CurrentLevel].DistansWallEcheOther;
        float temp_dis = 0;
        for (int i = 0; i < h; i++)
        {
            int rand_pos = UnityEngine.Random.Range(0, 3);
            pos_platform = new Vector3(3.14f, Levels[CurrentLevel].PadingDown, 0.0f);
            pos_platform.y = Mathf.Clamp(pos_platform.y + (temp_dis), Levels[CurrentLevel].PadingDown, h - Levels[CurrentLevel].PadingUp);
            if (pos_platform.y < h - Levels[CurrentLevel].PadingUp)
            {
                if (rand_pos == 0)
                {
                    pos_platform = new Vector3((distance - Levels[CurrentLevel].Offset_X) * -1, pos_platform.y, 0.0f);

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
                    pos_platform = new Vector3((distance - Levels[CurrentLevel].Offset_X) * 1, pos_platform.y, 0.0f);
                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].RightPlatform.Count);

                    var platform = Levels[CurrentLevel].RightPlatform[rand_platform].platform_prefab;
                    platform_spawned = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, Envirement.transform);
                    platform_spawned.transform.localScale = Levels[CurrentLevel].RightPlatform[rand_platform].Scale;
                }


                temp_dis += Levels[CurrentLevel].DistancePlatformEachOther;
            }
            platformsSpawned.Add(platform_spawned);

            
        }
        end = true;
        yield return new WaitWhile(() => end == false);
    }
    [Button("SpwanItem", ButtonSizes.Medium)]
    public IEnumerator SpwanItems()
    {
        bool end = false;
        float offset_y = 0;
        for (int i = 0; i < platformsSpawned.Count; i++)
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

        }
        end = true;
        yield return new WaitWhile(() => end == false);
    }
  //  [Button("SpwanAmmoBox", ButtonSizes.Medium)]
    public void SpwanBoxAmmo( Vector3 pos)
    {

        Instantiate(Levels[CurrentLevel].AmmoBox_prefab, pos, Levels[CurrentLevel].AmmoBox_prefab.transform.rotation, Envirement.transform);
        
    }
   // [Button("SpwanCoin", ButtonSizes.Medium)]
    public void SpwanCoin( Vector3 pos)
    {
        Instantiate(Levels[CurrentLevel].CoinBox_prefab, pos, Levels[CurrentLevel].CoinBox_prefab.transform.rotation, Envirement.transform);
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
    public PlatformWall Wall_prefab;
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

}
[Serializable]
public struct Platform
{

    public GameObject platform_prefab;
    public Vector3 Scale;
    public bool UseInEnvirment;
}


