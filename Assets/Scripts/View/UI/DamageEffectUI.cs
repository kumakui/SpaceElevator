using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DamageEffectUI : MonoBehaviour
{
    public ElevatorController elevatorController;

    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.clear;

        elevatorController.onElevatorDamageAction += onDamage;
    }

    // Update is called once per frame
    void Update()
    {
        _image.color = Color.Lerp(_image.color, Color.clear, Time.deltaTime);
    }

    private void onDamage(int HP)
    {
        if (HP == 100)
        {
            return;
        }
        _image.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
