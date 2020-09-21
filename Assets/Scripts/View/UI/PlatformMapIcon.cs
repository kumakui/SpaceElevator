using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMapIcon : MonoBehaviour
{
    public PlatformController platformController;
    public TutorialController tutorialController;

    private Material _material;

    private Color _defaultColor = new Color(0, 163, 255);
    private Color _destinationColor = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        tutorialController =
            GameObject.Find("TutorialController").GetComponent<TutorialController>();

        platformController.LoadAction += onLoad;
        tutorialController.platformIconUpdateAction += setDestination;

        _material = GetComponent<Renderer>().material;

        _material.color = _defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ロードの前に目的地アイコンだけを更新したいときに．tutorial03で使う．
    public void setDestination(string destName)
    {
        updateDestination(destName);
    }

    private void onLoad(LuggageData luggageData)
    {
        updateDestination(luggageData.Destination.name);
    }

    private void updateDestination(string destName)
    {
        //Load時にすべてのアイコンの色を一旦デフォルトに戻す
        _material.color = _defaultColor;

        //目的地のアイコンだけ色変更
        if (destName == gameObject.transform.root.gameObject.name)
        {
            var destPlatform = GameObject.Find(destName);

            var destinationMapIcon = destPlatform.transform.Find("PlatformMapIcon");
            destinationMapIcon.GetComponent<Renderer>().material.color = _destinationColor;
            _material.color = _destinationColor;
        }
    }
}
