using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using Sirenix.OdinInspector;
public class RingPower : MonoBehaviour
{
    public Disc CirclePower;
    public Line LineDirection;

    
    public float MaxScaleRange;
    public float PowerRange;
    public Vector3 DirectionForce;

    private SettingUI settingUI;
    public void Start()
    {
        settingUI = FindObjectOfType<SettingUI>();
        settingUI.OnChangeSetting += OnChangeSetting;
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
