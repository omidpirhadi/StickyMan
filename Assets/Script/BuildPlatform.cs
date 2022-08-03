using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class BuildPlatform : MonoBehaviour
{
    public int CurrentLevel;
    public List<LEVEL> Levels;
    
    [Button("CreateEnvirment",ButtonSizes.Medium)]
    public void CreateEnvirment()
    {
        GameObject envirment = new GameObject("Envirment");

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
                var endline = Instantiate(endline_prefab, new Vector3(0, (100 * i) + 100, 0), Quaternion.identity, envirment.transform);
            }


            var wall_L = Instantiate(wall_prefabs, pos_left, Quaternion.identity, envirment.transform);
            wall_L.name = "wall_left" + i;

            var wall_R = Instantiate(wall_prefabs, pos_right, Quaternion.identity, envirment.transform);
            wall_R.name = "wall_right" + i;

            //CreatePlatform(envirment.transform);
        }
      
    }
    [Button("CreatePlatform", ButtonSizes.Medium)]
    public void CreatePlatform(Transform envirment )
    {
        Vector3 pos_platform = new Vector3();
        int h = Levels[CurrentLevel].Height * 100;
        float distance = Levels[CurrentLevel].DistansWallEcheOther;
        float temp_dis = 0;
        for (int i = 0; i < h; i++)
        {
            int rand_pos = UnityEngine.Random.Range(0, 3);
            pos_platform = new Vector3(3.14f, Levels[CurrentLevel].PadingDown, 0.0f);
            pos_platform.y = Mathf.Clamp(pos_platform.y + (temp_dis), Levels[CurrentLevel].PadingDown, h- Levels[CurrentLevel].PadingUp);
            if (pos_platform.y <h- Levels[CurrentLevel].PadingUp)
            {
                if (rand_pos == 0)
                {
                    pos_platform = new Vector3(distance * -1, pos_platform.y, 0.0f);

                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].LeftPlatform.Count);

                    var platform = Levels[CurrentLevel].LeftPlatform[rand_platform].platform_prefab;
                    var p = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, envirment);
                }
                else if (rand_pos == 1)
                {
                    pos_platform = new Vector3(0, pos_platform.y, 0.0f);
                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].MedillePlatform.Count);

                    var platform = Levels[CurrentLevel].MedillePlatform[rand_platform].platform_prefab;
                    var p = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, envirment);
                }
                else if (rand_pos == 2)
                {
                    pos_platform = new Vector3(distance * 1, pos_platform.y, 0.0f);
                    int rand_platform = UnityEngine.Random.Range(0, Levels[CurrentLevel].RightPlatform.Count);

                    var platform = Levels[CurrentLevel].RightPlatform[rand_platform].platform_prefab;
                    var p = Instantiate(platform, pos_platform, platform.gameObject.transform.rotation, envirment);
                }

                temp_dis += Levels[CurrentLevel].DistancePlatformEachOther;
            }
        }
    }
    [Button("SpwanAmmoBox", ButtonSizes.Medium)]
    public void SpwanBoxAmmo(Transform envirment)
    {
        Vector3 pos_platform = new Vector3();
        int h = Levels[CurrentLevel].Height * 100;
        float temp_dis = 0;
        for (int i = 0; i < h; i++)
        {
            int rand_pos = UnityEngine.Random.Range(0, 2);
            pos_platform = new Vector3(0f, Levels[CurrentLevel].PadingDown, 0.0f);
            pos_platform.y = Mathf.Clamp(pos_platform.y + (temp_dis), Levels[CurrentLevel].PadingDown, h-Levels[CurrentLevel].PadingUp);
            if (rand_pos == 0)
            {
                pos_platform = new Vector3(0, pos_platform.y, 0.0f);
            }
            else
            {
                pos_platform = new Vector3(0, pos_platform.y, 0.0f);
            }
            temp_dis += Levels[CurrentLevel].DistanceAmmoBoxEachOther;
            if (pos_platform.y < h-Levels[CurrentLevel].PadingUp)
                Instantiate(Levels[CurrentLevel].AmmoBox_prefab, pos_platform, Levels[CurrentLevel].AmmoBox_prefab.transform.rotation, envirment);
        }
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
    [BoxGroup("Plastform Settings")]
    [BoxGroup("Plastform Settings/Padding")]
    public float PadingDown;
    [BoxGroup("Plastform Settings")]
    [BoxGroup("Plastform Settings/Padding")]
    public float PadingUp;
    [BoxGroup("Plastform Settings")]
    public List<Platform> LeftPlatform;
    [BoxGroup("Plastform Settings")]
    public List<Platform> MedillePlatform;
    [BoxGroup("Plastform Settings")]
    public List<Platform> RightPlatform;
    [BoxGroup("AmmoBox Settings")]
    public GameObject AmmoBox_prefab;
    [BoxGroup("AmmoBox Settings")]
    public float DistanceAmmoBoxEachOther;
    [BoxGroup("AmmoBox Settings")]
    public GameObject EndLine_prefab;

}
[Serializable]
public struct Platform
{

    public GameObject platform_prefab;
    public bool UseInEnvirment;
}