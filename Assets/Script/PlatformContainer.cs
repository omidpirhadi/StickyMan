using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SidePlatform { Left = 0 , Middle = 2, Right = 3}
public class PlatformContainer : MonoBehaviour
{

    public SidePlatform sidePlatform;
    public List<PlatformOnWall> LeftPlatforms;
    public List<PlatformOnWall> RightPlatforms;
    public float HeightBlackPlatform = 1100;
    public float HeightBluePlatform = 500;
    public float HeightRedPlatform = 100;
}
