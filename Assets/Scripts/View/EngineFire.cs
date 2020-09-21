using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EngineFire : MonoBehaviour
{
    public ElevatorController elevatorController;
    public Data data;
    private ParticleSystem _particle;
    private String _direction;
    private String _keyState;

    public Color startColor;
    public Color endColor;

    private ParticleSystem.MainModule _mainModule;
    private ParticleSystem.EmissionModule[] _childEmissionModules;

    // Start is called before the first frame update
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();

        elevatorController.keyPushAction += ChangeState;
        _direction = transform.tag;
        _mainModule = _particle.main;

        //子パーティクルのEmissionModuleの一覧を取得しておく．このやり方だと自身も含まれている模様．[0]が自身．[1]が子
        _childEmissionModules = GetComponentsInChildren<ParticleSystem>()
            .Select(p => p.emission).ToArray();

        // _childEmissionModules[1].enabled = false;
        SetEmissionEnable(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (_keyState == "up")
        // {
        //     if (_direction == "Up")
        //     {
        //         SetEmissionEnable(true);
        //     }
        //     else if (_direction == "Down")
        //     {
        //         SetEmissionEnable(false);
        //     }
        // }
        //
        // if (_keyState == "down")
        // {
        //     if (_direction == "Up")
        //     {
        //         SetEmissionEnable(false);
        //     }
        //     else if (_direction == "Down")
        //     {
        //         SetEmissionEnable(true);
        //     }
        // }
        //
        // if (_keyState == "none")
        // {
        //     SetEmissionEnable(false);
        // }
    }

    private void ChangeState(String key, float temp)
    {
        // _emission = _particle.emission;
        if (key == "up" && _direction == "Up")
        {
            SetEmissionEnable(true);
        }
        else if (key == "down" && _direction == "Down")
        {
            SetEmissionEnable(true);
        }
        else if (key == "none")
        {
            SetEmissionEnable(false);
        }

        if (key == "up")
        {
            _keyState = "up";
            // if (_direction == "Up")
            // {
            //     SetEmissionEnable(true);
            // }
            // else if (_direction == "Down")
            // {
            //     SetEmissionEnable(false);
            // }
        }

        if (key == "down")
        {
            _keyState = "down";
            // if (_direction == "Up")
            // {
            //     SetEmissionEnable(false);
            // }
            // else if (_direction == "Down")
            // {
            //     SetEmissionEnable(true);
            // }
        }

        if (key == "none")
        {
            _keyState = "none";
            // SetEmissionEnable(false);
        }

        _mainModule.startColor = Color.Lerp(startColor, endColor, (temp / data.T_MAX));
    }

    private void SetEmissionEnable(bool EmissionEnabled)
    {
        if (_childEmissionModules == null)
        {
            return;
        }

        foreach (var emissionModule in _childEmissionModules)
        {
            var module = emissionModule;
            module.enabled = EmissionEnabled;
        }
    }
}