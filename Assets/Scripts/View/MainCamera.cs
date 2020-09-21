using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    Vector3 diff;
    public GameObject target;
    public Camera camera;
    public Vector3 offset = Vector3.zero;

    public float baseWidth = 16.0f;
    public float baseHeight = 9.0f;

    private void Awake()
    {
        var scaleWidth = (Screen.height / this.baseHeight) * (this.baseWidth / Screen.width);
        var scaleRatio = Mathf.Max(scaleWidth, 1.0f);
        camera.fieldOfView = Mathf.Atan(Mathf.Tan(this.camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * scaleRatio) * 2.0f * Mathf.Rad2Deg;
    }

    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            (target.transform.position + offset) - diff,
            0.8f);
    }
}
