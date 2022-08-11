using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ControllExplosionEffectBullet : MonoBehaviour
{

    public void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }
}
