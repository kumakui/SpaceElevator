using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DockingProgressBar : MonoBehaviour
{
    private Image ProgressBar;
    public ElevatorController ElevatorController;
    // Start is called before the first frame update
    void Start()
    {
        ProgressBar = GetComponent<Image>();
        ElevatorController.elevatorDockingProgressAction += updateDockingProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateDockingProgress(float progress)
    {
        ProgressBar.fillAmount = progress;
    }


}
