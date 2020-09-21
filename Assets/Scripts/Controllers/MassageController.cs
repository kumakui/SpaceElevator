using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MassageController : MonoBehaviour
{
    private MassageData _massageData;
    private bool _isMassageShowing = false;

    public UnityAction<MassageData, EventName> startEventAction;
    public Func<bool> ShowNextMassage;


    // Start is called before the first frame update
    void Start()
    {
        _massageData = (MassageData) Resources.Load("MassageData");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)  && _isMassageShowing)
        {
            if (ShowNextMassage.Invoke())
            {
                //イベントが終了したとき
                _isMassageShowing = false;
            }
        }
    }

    public void startEvent(EventName eventName)
    {
        _isMassageShowing = true;
        startEventAction.Invoke(_massageData, eventName);
        ShowNextMassage.Invoke();
    }
}
