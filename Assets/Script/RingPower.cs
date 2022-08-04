using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using Sirenix.OdinInspector;
using DG.Tweening;
public class RingPower : MonoBehaviour
{
    public Disc CirclePower;
    public Line LineDirection;

    
    public float MaxScaleRange;
    public float PowerRange;
    public Vector3 DirectionForce;

    private SettingUI settingUI;
    private Tween RangFillWithTime;
    public float Step = 0.1f;
    public float speed = 2;
    public bool autorang = false;
    public void Start()
    {
        settingUI = FindObjectOfType<SettingUI>();
        settingUI.OnChangeSetting += OnChangeSetting;
    }
    public void Update()
    {
        if(autorang)
        {
           
            SetRangeWithTime();
            
        }
    }
    private void OnChangeSetting()
    {
       PowerRange =  settingUI.powerrange;
    }

    [Button("SetRange", ButtonSizes.Medium)]
    public void SetRange(float r)
    {
        
        var radius = Mathf.Clamp(r, 0, MaxScaleRange);
        PowerRange = Mathf.Clamp(r / MaxScaleRange, 0, 1); 
        CirclePower.Radius = radius;
        LineDirection.End = new Vector3(radius, 0, 0);
    }
    public void SetRangeWithTime()
    {
        Step = Mathf.Clamp(Step, 0, 5);
        var radius = Mathf.Clamp(Step, 0, MaxScaleRange);
        PowerRange = Mathf.Clamp(Step / MaxScaleRange, 0, 1);
        CirclePower.Radius = radius;
        LineDirection.End = new Vector3(radius, 0, 0);
        Step += 0.1f * Time.unscaledDeltaTime *speed;
    }
    public void KillSetRangWithTime()
    {
        autorang = false;
        Step = 0.0f;
       // RangFillWithTime.Kill();
    }
    public void PlaySetRangWithTime()
    {
        autorang = true;
        Step = 0.0f;
        // RangFillWithTime.Kill();
    }
    private void OnApplicationPause(bool pause)
    {

    }
    public void SetPosition(Vector3 Pos)
    {
        this.transform.position = Pos;
    }
    public void SetRotation( Vector3 target)
    {
        var dir = (target - this.transform.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle );
        
    }
    public Vector3 CalculateDirectionForce()
    {
        var dir = ( LineDirection.transform.TransformDirection( LineDirection.End) - LineDirection.transform.TransformDirection(LineDirection.Start)).normalized;
       // Debug.Log($"Dir = {dir},Pos = {LineDirection.End}");
        return dir;
    }
    public void Reset()
    {
        this.transform.position = new Vector3(0, -1000, 0);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        SetRange(0);
    }
}
