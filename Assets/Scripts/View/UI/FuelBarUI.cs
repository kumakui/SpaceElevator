using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarUI : MonoBehaviour
{
    public ElevatorController elevatorController;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        elevatorController.elevatorFuelChangeAction += OnFuelChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnFuelChange(float fuel)
    {
        slider.value = fuel / 100;
    }
}
