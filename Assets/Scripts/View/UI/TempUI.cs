using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempUI : MonoBehaviour
{
    private Image _tempBar;
    public ElevatorController elevatorController;
    public Data data;

    public Color startColor;
    public Color endColor;


    // Start is called before the first frame update
    void Start()
    {
        _tempBar = GetComponent<Image>();
        elevatorController.elevatorTempChangeAction += UpdateTempUi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTempUi(float temp)
    {
        _tempBar.fillAmount = temp / (data.T_MAX * 2);
        _tempBar.color = Color.Lerp(startColor, endColor, (temp / data.T_MAX));
    }
}
