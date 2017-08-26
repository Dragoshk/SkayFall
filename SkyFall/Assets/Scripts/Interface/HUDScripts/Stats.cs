using UnityEngine;  
using System;
[Serializable]
public class Stats
{
    [SerializeField]
    private Bars _bar;
    [SerializeField]
    private float _maxValue;
    [SerializeField]
    private float _currentVal;

    public float CurrentVal
    {
        get
        {
            return _currentVal;
        }

        set
        {
            _currentVal = Mathf.Clamp(value,0,MaxValue);
            _bar.Value = _currentVal;
        }
    }
    
    public float MaxValue
    {
        get
        {
            return _maxValue;
        }

        set
        {
            _maxValue = value;
            _bar.MaxValue = _maxValue;
        }
    }
    
    public void Initialize()
    {
        MaxValue = _maxValue;
        CurrentVal = _currentVal;
    }    
}
