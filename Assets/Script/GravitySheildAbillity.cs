using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GravitySheildAbillity : MonoBehaviour
{
    public ParticleSystem GravityShhieldEffect;
    public EndLine endLine;

    void OnEnable()
    {
        GravityShhieldEffect.Play(true);
        endLine.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        GravityShhieldEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        endLine.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider t)
    {
   
        if (t.tag == "Body" )
        {
            DOVirtual.DelayedCall(0.05f, () => { Physics.gravity = new Vector3(0, 10, 0); });
            Debug.Log("Active Shield");
        }
    }
    private void OnTriggerExit(Collider t)
    {

        if (t.tag == "Body")
        {
            DOVirtual.DelayedCall(1, () => { Physics.gravity = new Vector3(0, -10, 0); });
            Debug.Log("not Active Shield");

        }
    }
}
