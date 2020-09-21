using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// using RedBlueGames.Tools.TextTyper;
using UnityEngine;

public class Message : MonoBehaviour
{
    // public MassageController MassageController;
    //
    // private TextTyper _textTyper;
    // private List<MassageData.Param> _massageList;
    // //現在読み込んでいるエクセルの行数．初期値0．
    // private int _massageCount;
    //
    //
    // // private TextData _textData;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     _textTyper = GetComponent<TextTyper>();
    //
    //     MassageController.startEventAction += SetMassage;
    //     MassageController.ShowNextMassage = ShowNextMassage;
    //
    //     // _massage = (MassageData) Resources.Load("MassageData");
    //     //
    //     // foreach (var row in _massage.sheets[0].list)
    //     // {
    //     //     Debug.Log(row.Text + "  " + row.Color);
    //     // }
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //
    // }
    //
    // private void SetMassage(MassageData massageData, EventName eventName)
    // {
    //     _massageList = massageData.sheets[(int) eventName].list;
    //     _massageCount = 0;
    // }
    //
    // private bool ShowNextMassage()
    // {
    //     String massage = String.Empty;
    //
    //     while (true)
    //     {
    //         String color = _massageList[_massageCount].Color;
    //         massage += "<color=" + color + ">";
    //         massage += _massageList[_massageCount].Text;
    //         massage += "</color>";
    //         _massageCount++;
    //
    //         //次の行が空白なら，このイベントは終了
    //         if (_massageList.Count == _massageCount)
    //         {
    //             _textTyper.TypeText(massage);
    //             _massageList = null;
    //             return true;
    //         }
    //
    //         //次の行のtextが"br"ならこのセンテンスはこれで終了
    //         if (_massageList[_massageCount].Text == "br")
    //         {
    //             _textTyper.TypeText(massage);
    //             return false;
    //         }
    //
    //     }
    //
    //
    // }
}
