using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private bool _previousFogState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPreRender()
    {
        _previousFogState = RenderSettings.fog;
        RenderSettings.fog = false;
    }

    private void OnPostRender()
    {
        //fogを表示しない
        RenderSettings.fog = _previousFogState;
    }
}
