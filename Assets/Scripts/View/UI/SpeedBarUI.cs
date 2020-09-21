using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBarUI : MonoBehaviour
{
    private Image _speedBar;
    public ElevatorController ElevatorController;

    // Start is called before the first frame update
    void Start()
    {
        _speedBar = GetComponent<Image>();
        ElevatorController.elevatorSpeedChangeAction += UpdateSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSpeed(float speed)
    {
        _speedBar.fillAmount = speed / (50 * 2);

    }
}
