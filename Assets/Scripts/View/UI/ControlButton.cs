using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    public float lineWidth;
    public float lineLength;
    public ElevatorController elevatorController;
    public PlatformController platformController;

    private LineRenderer _lineRenderer;
    private float _width;
    private float _height;
    private Vector3 _center;
    private Camera _mainCamera;
    private Vector3[] _lines;

    // Start is called before the first frame update
    void Start()
    {
        elevatorController.dockAction += Disable;
        platformController.UndockAction += Enable;

        gameObject.SetActive(false);
        // _lineRenderer = GetComponent<LineRenderer>();
        // _mainCamera = Camera.main;
        //
        // var rectTransform = GetComponent<RectTransform>();
        // _width = rectTransform.sizeDelta.x;
        // _height = rectTransform.sizeDelta.y;
        // _center = rectTransform.position;
        //
        // _lineRenderer.startWidth = lineWidth;
        // _lineRenderer.loop = true;
        // _lineRenderer.useWorldSpace = true;
        //
        // _lines = new Vector3[4];
        // var z = _mainCamera.transform.position.z;
        //
        // _lines[0] = new Vector3(_center.x - _width / 2, _center.y - _height / 2, z);
        // _lines[1] = new Vector3(_center.x + _width / 2, _center.y - _height / 2, z);
        // _lines[2] = new Vector3(_center.x + _width / 2, _center.y + _height / 2, z);
        // _lines[3] = new Vector3(_center.x - _width / 2, _center.y + _height / 2, z);
    }

    // Update is called once per frame
    void Update()
    {
        // var Positions = _lines.Select(line => _mainCamera.ScreenToWorldPoint(line))
        //     .Select( position =>
        //     {
        //         position.z = 0;
        //         return position;
        //     })
        //     .ToArray();
        //
        // _lineRenderer.SetPositions(Positions);


    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable(Vector3 dockingPos)
    {
        gameObject.SetActive(false);
    }
}