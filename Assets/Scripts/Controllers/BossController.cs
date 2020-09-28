using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    public GameObject EntryObject;
    public Dragon Dragon;
    public GameObject explosionParticle;
    public GameObject[] hitParticles = new GameObject[2];
    public Flowchart flowchart;
    public MainCamera mainCamera;
    public ElevatorController elevatorController;
    public PlatformController platformController;
    public GameController gameController;
    public GameObject turret;
    public Data data;


    public UnityAction BossFightStartAction;
    public UnityAction<ElevatorData> onElevatorRefreshAction;
    public UnityAction<int> onBossDamageAction;
    public UnityAction onBossDefeatAction;
    public UnityAction onElevatorDefeatedAction;
    public UnityAction onRestartFightAction;

    private bool _boss01;
    private bool _boss02;
    private bool _boss03;

    private ElevatorData _elevatorData;
    private int _HP = 100;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnElevatorRefresh(ElevatorData elevatorData)
    {
        if (elevatorData != null)
        {
            _elevatorData = elevatorData;
        }

        if (onElevatorRefreshAction != null)
        {
            onElevatorRefreshAction.Invoke(elevatorData);
        }
    }

    public void AwakeBoss()
    {
        UpdateVariable();
        mainCamera.target = EntryObject;
        StartCoroutine(Wait(0.5f, () =>
        {
            var explosionPos = EntryObject.transform.position + new Vector3(0, 0, -100);

            Instantiate(explosionParticle, explosionPos, Quaternion.identity);

            EntryObject.SetActive(false);
            StartCoroutine(Wait(0.2f, () =>
            {
                Dragon.gameObject.SetActive(true);
                gameController.OnBossAwake();
                if (_boss01 && !_boss02)
                {
                    flowchart.SendFungusMessage("Boss02");
                }
            }));
        }));
    }

    public void StartFight()
    {
        UpdateVariable();
        mainCamera.offset = new Vector3(70, 0, 0);
        mainCamera.target = GameObject.Find("Elevator");
        // mainCamera.camera.orthographicSize = 200f;
        mainCamera.camera.fieldOfView = 35f;//デフォルトは19

        Dragon.transform.position = _elevatorData.Position + new Vector3(120, 0, 0);
        Dragon.transform.rotation = Quaternion.Euler(0, -90, 0);
        Dragon.StartFight();

        BossFightStartAction.Invoke();
        elevatorController.OnBossStart();
        platformController.OnBossStart();

        turret.SetActive(true);
    }

    public void getDamage(Collider other, Vector3 bossPos)
    {
        _HP -= data.GunDamage;
        onBossDamageAction.Invoke(_HP);

        //ヒットエフェクト
        foreach (var particle in hitParticles)
        {
            var oneShot = Instantiate(particle,
                other.transform.position + new Vector3(0, 0, 50),
                Quaternion.identity);
            Destroy(oneShot, 10);
        }

        if (_HP <= 0)
        {
            BossDefeated();
        }
    }

    private void BossDefeated()
    {
        UpdateVariable();
        onBossDefeatAction.Invoke();
        mainCamera.offset = new Vector3(0, 0, 0);
        mainCamera.target = GameObject.Find("DragonSoulEaterRedPBR");

        StartCoroutine(Wait(1f, () => { flowchart.SendFungusMessage("Boss03"); }));
    }

    public void ElevatorDefeated()
    {
        onElevatorDefeatedAction.Invoke();
        flowchart.SendFungusMessage("Boss04");
    }


    public void RestartFight()
    {
        _HP = 100;
        onRestartFightAction.Invoke();
    }

    private IEnumerator Wait(float waitSecond, Action action)
    {
        yield return new WaitForSeconds(waitSecond);
        action();
    }

    private IEnumerator Loop(float loopSecond, Action action)
    {
        while (true)
        {
            yield return new WaitForSeconds(loopSecond);
            action();
        }
    }

    private void UpdateVariable()
    {
        var Boss01 = flowchart.GetVariable("Boss01") as BooleanVariable;
        _boss01 = Boss01.Value;
        var Boss02 = flowchart.GetVariable("Boss02") as BooleanVariable;
        _boss02 = Boss02.Value;
        var Boss03 = flowchart.GetVariable("Boss03") as BooleanVariable;
        _boss03 = Boss03.Value;
    }

    public void DebugBoss()
    {
        Dragon.gameObject.SetActive(true);

        StartCoroutine(Wait(0.5f, () =>
        {
            mainCamera.offset = new Vector3(70, 0, 0);
            mainCamera.target = GameObject.Find("Elevator");
            mainCamera.camera.orthographicSize = 200f;
            mainCamera.camera.fieldOfView = 35f;

            Dragon.transform.position = _elevatorData.Position + new Vector3(120, 0, 0);
            Dragon.transform.rotation = Quaternion.Euler(0, -90, 0);
            Dragon.StartFight();

            BossFightStartAction.Invoke();
            elevatorController.OnBossStart();
            platformController.OnBossStart();

            turret.SetActive(true);
        }));
    }
}