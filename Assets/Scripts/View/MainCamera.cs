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
    public float basePixelPerUnit = 100f;

    private Camera _camera;
    private int _width = -1;
    private int _height = -1;

    private void Awake()
    {
        // //アスペクト比固定
        // var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
        // var width = (this.baseWidth * scale) / Screen.width;
        // var height = (this.baseHeight * scale) / Screen.height;
        // this.camera.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);

        _camera = GetComponent<Camera>();
        UpdateCamera();

    }

    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraWithCheck();
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            (target.transform.position + offset) - diff,
            0.8f);
    }

    void UpdateCameraWithCheck()
    {
        if(_width == Screen.width && _height == Screen.height){
            return;
        }
        UpdateCamera();
    }

    void UpdateCamera()
    {
        float screen_w = (float)Screen.width;
        float screen_h = (float)Screen.height;
        float target_w = (float)baseWidth;
        float target_h = (float)baseHeight;

        //アスペクト比
        float aspect =  screen_w / screen_h;
        float targetAcpect = target_w / target_h;
        float orthographicSize = (target_h / 2f / basePixelPerUnit);

        //縦に長い
        if (aspect < targetAcpect)
        {
            float bgScale_w = target_w / screen_w;
            float camHeight = target_h / (screen_h * bgScale_w);
            _camera.rect = new Rect( 0f, (1f-camHeight)*0.5f, 1f, camHeight);
        }
        // 横に長い
        else
        {
            // カメラのorthographicSizeを横の長さに合わせて設定しなおす
            float bgScale = aspect / targetAcpect;
            orthographicSize *= bgScale;

            float bgScale_h = target_h / screen_h;
            float camWidth = target_w / (screen_w * bgScale_h);
            _camera.rect = new Rect( (1f-camWidth)*0.5f, 0f, camWidth, 1f);
        }

        _camera.orthographicSize = orthographicSize;

        _width = Screen.width;
        _height = Screen.height;
    }
}
