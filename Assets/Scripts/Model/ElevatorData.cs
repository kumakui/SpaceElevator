using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorData : MonoBehaviour
{
    public Data data;

    public float Speed { get; set; } = 0f;

    public Boolean IsDockingProgress { get; set; } = false;

    public GameObject DockingPlatform { get; set; } = null;

    public Vector3 Position { get; set; }

    public Boolean IsDocked { get; set; } = false;

    public LuggageData Luggage { get; set; } = null;

    public bool IsDockingable { get; set; } = true;

    public bool IsPaused { get; set; } = false;

    public bool InBattle { get; set; } = false;

    public int HP { get; set; } = 100;

    private float _temp = 0f;

    public float Temp
    {
        get { return _temp; }
        set
        {
            //tempに負値は禁止
            if (value < 0)
            {
                _temp = 0;
            }
            //温度最大値1000
            else if(value > data.T_MAX)
            {
                _temp = data.T_MAX;
            }
            else
            {
                _temp = value;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Luggage = new LuggageData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
