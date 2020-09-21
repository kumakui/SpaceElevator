using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    private Text text;

    public ElevatorController ElevatorController;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        ElevatorController.elevatorSpeedChangeAction += UpdateSpeedText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateSpeedText(float speed)
    {
        text.text = (speed / 3.6f).ToString("F0") + "km/h";
    }
}
