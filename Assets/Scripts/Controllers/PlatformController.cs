using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlatformController : MonoBehaviour
{
    public TutorialController tutorialController;

    public UnityAction<LuggageData> UnloadAction;
    public UnityAction UndockAction;
    public UnityAction<LuggageData> LoadAction;
    public UnityAction onDialogFinishAction;

    private LuggageData _oldLuggageData;
    private GameObject _dockingPlatform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Dock(GameObject dockingPlatform, LuggageData luggageData)
    {
        _dockingPlatform = dockingPlatform;
        if (luggageData.Destination == dockingPlatform)
        {
            UnLoad(luggageData);

            _oldLuggageData = luggageData;
        }
        else
        {
            onDialogFinishAction.Invoke();
        }
    }

    private void UnLoad(LuggageData luggageData)
    {
        UnloadAction.Invoke(luggageData);
    }

    public void OnUnloadFinished()
    {
        if (tutorialController.OnUnloadFinished(_dockingPlatform))
        {
            //チュートリアルがあるとき
            return;
        }

        //チュートリアルがないとき
        Load(null);
    }

    public void Load([CanBeNull] LuggageData luggageData)
    {
        if (luggageData == null)
        {
            //ドッキング時にチュートリアル等が表示されず，ランダムで次の目的地を決めるとき
            luggageData = new LuggageData
            {
                Amount = 5, Destination = NextDest(_oldLuggageData.Destination)
            };
        }

        LoadAction.Invoke(luggageData);
    }

    private void Undock()
    {
        _dockingPlatform = null;
        UndockAction.Invoke();
    }

    public void OnUndockClicked()
    {
        Undock();
    }

    //ダイアログをすべて表示し終わった
    //called from dialog flowchart and tutorial flowchart
    public void onDialogFinish()
    {
        onDialogFinishAction.Invoke();
    }

    public void OnBossStart()
    {
        Undock();
    }

    private GameObject NextDest(GameObject oldLuggageDest)
    {
        int dest;
        do
        {
            dest = Random.Range(1, 3 + 1);
        } while (oldLuggageDest.name.EndsWith(dest.ToString()));

        GameObject destPlatform = GameObject.Find("Platform" + dest);
        return destPlatform;
    }
}