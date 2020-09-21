using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{

    public PlatformController platformController;
    public ElevatorController elevatorController;
    public Flowchart flowchart;

    public UnityAction<string> platformIconUpdateAction;

    private bool _tutorial01;
    private bool _tutorial02;
    private bool _tutorial03;
    private bool _tutorial04;
    private bool _boss01;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //チュートリアルがあるときはtrueを返す
    //ここに書かれたチュートリアルの最後で必ずLoadすること
    public bool OnUnloadFinished(GameObject dockingPlatform)
    {
        UpdateVariables();

        //ボスデバッグ用
        // if (!_boss01 && dockingPlatform.name.Equals("Platform2") )
        // {
        //     flowchart.SendFungusMessage("Boss01");
        //     return true;
        // }

        if (!_tutorial03 && dockingPlatform.name.Equals("Platform2"))
        {
            flowchart.SendFungusMessage("Tutorial03");
            return true;
        }


        if (!_tutorial04 && dockingPlatform.name.Equals("Platform3"))
        {
            flowchart.SendFungusMessage("Tutorial04");
            return true;
        }

        if (!_boss01 && dockingPlatform.name.Equals("Platform4") )
        {
            flowchart.SendFungusMessage("Boss01");
            return true;
        }

        return false;
    }

    public void onEnterCollision(Collider other)
    {
        UpdateVariables();
        if (!_tutorial02 && other.CompareTag("TutorialCollider") && other.transform.root.name == "Platform2")
        {
            flowchart.SendFungusMessage("Tutorial02");
        }
    }

    //called from tutorial flowchart
    //Unload()に関してはチュートリアルの有無に関わらず目的地のプラットフォーム到着時，はじめに必ずUnloadするので，チュートリアルダイアログ内からUnloadすることはない
    public void Load(int amount, string dest)
    {
        LuggageData luggageData = new LuggageData
        {
            Amount = amount, Destination = GameObject.Find(dest)
        };

        platformController.Load(luggageData);
    }


    public void Pause()
    {
        elevatorController.PauseElevator();
    }

    public void Resume()
    {
        elevatorController.ResumeElevator();
    }

    public void setDestIcon(string destName)
    {
        platformIconUpdateAction.Invoke(destName);
    }

    private void UpdateVariables()
    {
        var Tutorial01 = flowchart.GetVariable("Tutorial01") as BooleanVariable;
        _tutorial01 = Tutorial01.Value;

        var Tutorial02 = flowchart.GetVariable("Tutorial02") as BooleanVariable;
        _tutorial02 = Tutorial02.Value;

        var Tutorial03 = flowchart.GetVariable("Tutorial03") as BooleanVariable;
        _tutorial03 = Tutorial03.Value;

        var Tutorial04 = flowchart.GetVariable("Tutorial04") as BooleanVariable;
        _tutorial04 = Tutorial04.Value;

        var Boss01 = flowchart.GetVariable("Boss01") as BooleanVariable;
        _boss01 = Boss01.Value;
    }
}
