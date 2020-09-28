using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public BossController bossController;
    public ElevatorController elevatorController;
    public Slider slider;
    public Data data;
    public Canvas canvas;

    private Camera _camera;

    private

        // Start is called before the first frame update
        void Start()
    {
        _camera = Camera.main;
        slider.value = 1f;
        canvas.transform.rotation = _camera.transform.rotation;
        bossController.onBossDamageAction += OnBossDamage;
        bossController.BossFightStartAction += OnBossStart;
        elevatorController.onElevatorDamageAction += OnElevatorDamage;
        bossController.onBossDefeatAction += FightFinish;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = _camera.transform.rotation;
    }

    private void OnBossStart()
    {
        gameObject.SetActive(true);
    }

    private void OnBossDamage(int hp)
    {
        if (transform.CompareTag("BossHP"))
        {
            slider.value = (float) hp / 100;
        }
    }

    private void OnElevatorDamage(int hp)
    {
        if (transform.CompareTag("ElevatorHP"))
        {
            slider.value = (float) hp / 100;
        }
    }

    private void FightFinish()
    {
        gameObject.SetActive(false);
    }
}