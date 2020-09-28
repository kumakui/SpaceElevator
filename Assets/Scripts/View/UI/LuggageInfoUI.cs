using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuggageInfoUI : MonoBehaviour
{
    private Text _text;
    public ElevatorController elevatorController;
    public BossController bossController;

    // Start is called before the first frame update
    void Start()
    {
        elevatorController.luggageInfoUpdateAction += UpdateLuggageInfo;
        bossController.BossFightStartAction += onBossStart;
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateLuggageInfo(int amount, GameObject dest)
    {
        if (dest == null)
        {
            return;
        }
        string platformName = dest.tag;
        _text.text = "目的地: " + platformName + "\r\n" + "重さ: " + amount + "トン";
    }

    private void onBossStart()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
