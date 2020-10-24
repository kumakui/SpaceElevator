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
        //アスペクト比固定
        var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
        var width = (this.baseWidth * scale) / Screen.width;
        var height = (this.baseHeight * scale) / Screen.height;
        this.camera.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);


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
