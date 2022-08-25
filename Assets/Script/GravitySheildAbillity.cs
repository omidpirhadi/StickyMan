using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GravitySheildAbillity : MonoBehaviour
{
    public ParticleSystem GravityShhieldEffect;
    public EndLine endLine;
    public float Duration = 5;
    void OnEnable()
    {
        ActiveAbility(true);
        
       
    }

    private void ActiveAbility(bool enable)
    {
        if (enabled == true)
        {
            GravityShhieldEffect.Play(true);
            endLine.gameObject.SetActive(false);
            DOVirtual.DelayedCall(Duration, () => {
                GravityShhieldEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                endLine.gameObject.SetActive(true);
                Physics.gravity = new Vector3(0, -10, 0);
                gameObject.SetActive(false);
            });
           
        }

        
    }
    private void OnTriggerEnter(Collider t)
    {
   
        if (t.tag == "Body" )
        {
            DOVirtual.DelayedCall(0.05f, () => { Physics.gravity = new Vector3(0, 10, 0); });
            //Debug.Log("Active Shield");
        }
    }
    private void OnTriggerExit(Collider t)
    {

        if (t.tag == "Body")
        {
            DOVirtual.DelayedCall(1, () => { Physics.gravity = new Vector3(0, -10, 0); });
           // Debug.Log("not Active Shield");

        }
    }
}
