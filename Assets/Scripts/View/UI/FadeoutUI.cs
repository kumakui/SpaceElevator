using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutUI : MonoBehaviour
{
    public ElevatorController elevatorController;
    public float fadeDuration;

    private Image _image;
    private string _fadeState = String.Empty;
    private Color _color;
    private float _startTime;

    // Start is called before the first frame update
    void Start()
    {
        elevatorController.ReturnToGroundAction += RunOutFuel;

        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeState == "fadeout")
        {
            _color.a = (Time.time - _startTime) / fadeDuration;
            if (_color.a >= 1f)
            {
                _fadeState = String.Empty;
                return;
            }

            _image.color = new Color(0, 0, 0, _color.a);
        }

        if (_fadeState == "fadein")
        {
            _color.a = 1f - (Time.time - _startTime) / fadeDuration;
            _image.color = new Color(0, 0, 0, _color.a);
            if (_color.a <= 0f)
            {
                _fadeState = String.Empty;
                return;
            }
        }
    }

    private void RunOutFuel()
    {
        _fadeState = "fadeout";
        _startTime = Time.time;

        StartCoroutine(Wait(3, () =>
        {
            _startTime = Time.time;
            _fadeState = "fadein";
        }));
    }

    private IEnumerator Wait(float waitSecond, Action action)
    {
        yield return new WaitForSeconds(waitSecond);
        action();
    }
}