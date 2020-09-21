using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UndockButtonUI : MonoBehaviour
{
    public PlatformController PlatformController;
    public ElevatorController ElevatorController;
    private GameObject _button;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<GameObject>();
        PlatformController.onDialogFinishAction += OnDialogFinish;
        PlatformController.UndockAction += OnUndock;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDialogFinish()
    {
        gameObject.SetActive(true);
    }

    private void OnUndock()
    {
        gameObject.SetActive(false);
    }
}
