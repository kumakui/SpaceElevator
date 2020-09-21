using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public PlatformController PlatformController;
    private Flowchart _flowchart;

    // Start is called before the first frame update
    void Start()
    {
        _flowchart = GetComponent<Flowchart>();
        PlatformController.UnloadAction += ShowUnloadDialog;
        PlatformController.LoadAction += ShowLoadDialog;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUnloadFinished()
    {
        PlatformController.OnUnloadFinished();
    }

    public void OnLoadFinished()
    {
        PlatformController.onDialogFinish();
    }

    private void ShowUnloadDialog(LuggageData luggageData)
    {

        _flowchart.SetIntegerVariable("LuggageAmount", luggageData.Amount);
        _flowchart.SetStringVariable("LuggageDest", luggageData.Destination.tag);
        _flowchart.SendFungusMessage("ShowUnloadDialog");
    }

    private void ShowLoadDialog(LuggageData luggageData)
    {

        _flowchart.SetIntegerVariable("LuggageAmount", luggageData.Amount);
        _flowchart.SetStringVariable("LuggageDest", luggageData.Destination.tag);
        _flowchart.SendFungusMessage("ShowLoadDialog");

        // _flowchart.SetBooleanVariable("ShowTogether", false);
    }
}
