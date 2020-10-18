using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using Random = UnityEngine.Random;

public class Elevator : MonoBehaviour
{
    private Rigidbody _rb;
    private AudioSource[] _audioSources;
    private float _damageCount = 0f; //前回ダメージを受けてからの経過時間
    public float invincibleTime; //被弾後の無敵時間

    public ElevatorData elevatorData;

    public Data data;
    public ElevatorController elevatorController;
    public PlatformController platformController;
    public BossController bossController;
    public Flowchart flowchart;

    public GameObject upFire;
    public GameObject downFire;
    private ParticleSystem.EmissionModule _upEmission;
    private ParticleSystem.EmissionModule _downEmission;

    // public float soundVolume;
    public float soundMinPitch;
    public float soundMaxPitch;

    public ControlButton controlButton;
    public Gun gun;

    private PointerState _pointerState = PointerState.None;

    public float fuelConsumption;

    private enum PointerState
    {
        Up,
        Down,
        None
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSources = GetComponents<AudioSource>();
        elevatorData.Position = GetComponent<Transform>().position;
        elevatorController.dockAction += Dock;
        platformController.UnloadAction += Unload;
        platformController.UndockAction += UnDock;
        platformController.LoadAction += Load;
        elevatorController.elevatorPauseAction += Pause;
        elevatorController.elevatorResumeAction += Resume;
        bossController.onBossDefeatAction += OnBossDefeat;
        bossController.onRestartFightAction += OnRestartBossFight;
        elevatorController.ReturnToGroundAction += ReturnToGround;

        _upEmission = upFire.GetComponent<ParticleSystem>().emission;
        _downEmission = downFire.GetComponent<ParticleSystem>().emission;

        var platform1 = GameObject.Find("Platform1");
        var initialPos = platform1.transform.Find("PlatformCollider").position;
        elevatorData.DockingPlatform = platform1;
        Dock(initialPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (elevatorData.IsDockingProgress && !_audioSources[1].isPlaying)
        {
            _audioSources[1].Play();
        }

        if (!elevatorData.IsDockingProgress && _audioSources[1].isPlaying)
        {
            _audioSources[1].Stop();
        }

        _damageCount += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //エディター用入力検知
        // if (Input.GetKey("up"))
        // {
        //     _pointerState = PointerState.Up;
        // }
        // else if (Input.GetKey("down"))
        // {
        //     _pointerState = PointerState.Down;
        // }
        // else
        // {
        //     _pointerState = PointerState.None;
        // }

        Vector3 thrust = Vector3.zero;

        if (_pointerState == PointerState.Up && !elevatorData.IsDocked && !elevatorData.IsPaused)
        {
            elevatorController.OnKeyStateChanged("up");
            // _upEmission.enabled = true;

            thrust = CulcThrust();

            if (_rb.velocity.y > 0)
            {
                _rb.AddForce(thrust);
            }
            else
            {
                _rb.AddForce(thrust * 0.7f);
            }

            _audioSources[0].mute = false;
            _audioSources[0].pitch =
                Mathf.Lerp(soundMinPitch, soundMaxPitch, elevatorData.Temp / data.T_MAX);

            if (!elevatorData.InBattle)
            {
                elevatorData.fuel -= fuelConsumption;
            }

            if (elevatorData.fuel <= 0f)
            {
                RunOutFuel();
            }
        }
        else if (_pointerState == PointerState.Down && !elevatorData.IsDocked &&
                 !elevatorData.IsPaused)
        {
            elevatorController.OnKeyStateChanged("down");
            // _downEmission.enabled = true;

            thrust = -CulcThrust();


            if (_rb.velocity.y < 0)
            {
                _rb.AddForce(thrust);
            }
            else
            {
                _rb.AddForce(thrust * 0.7f);
            }

            _audioSources[0].mute = false;
            _audioSources[0].pitch =
                Mathf.Lerp(soundMinPitch, soundMaxPitch, elevatorData.Temp / data.T_MAX);

            if (!elevatorData.InBattle)
            {
                elevatorData.fuel -= fuelConsumption;
            }
            if (elevatorData.fuel <= 0f)
            {
                RunOutFuel();
            }
        }
        else if (_pointerState == PointerState.None)
        {
            elevatorController.OnKeyStateChanged("none");
            // _upEmission.enabled = false;
            // _downEmission.enabled = false;

            elevatorData.Temp -= data.T_cooling_rate;

            _audioSources[0].mute = true;
        }

        elevatorData.Speed = _rb.velocity.magnitude;
        elevatorData.Position = _rb.position;
        elevatorController.Refresh(elevatorData);
    }

    private void OnTriggerEnter(Collider other)
    {
        elevatorController.onEnterCollision(other);

        if (other.CompareTag("PlatformCollider"))
        {
            elevatorData.DockingPlatform = other.transform.root.gameObject;
            if (!elevatorData.IsDocked && elevatorData.IsDockingable && !elevatorData.InBattle)
            {
                elevatorData.IsDockingProgress = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlatformCollider"))
        {
            elevatorData.DockingPlatform = null;
            elevatorData.IsDockingProgress = false;

            elevatorData.IsDockingable = true;
        }
    }

    private Vector3 CulcThrust()
    {
        var P_slope = data.P_slope;
        var T = elevatorData.Temp;
        var P_0 = data.P_0;

        elevatorData.Temp += data.T_0 * Time.deltaTime;
        // var thrust = Vector3.up * (data.P_slope * (data.P_0 + elevatorData.Temp));

        var force = P_slope * T + P_0;

        return force * Vector3.up;
    }

    private void Dock(Vector3 pos)
    {
        elevatorData.IsDocked = true;
        elevatorData.IsDockingProgress = false;
        _rb.velocity = Vector3.zero;
        // _rb.isKinematic = true;
        _rb.useGravity = false;
        _rb.position = pos;
        _pointerState = PointerState.None;

        if (elevatorData.DockingPlatform == null) return;

        if (elevatorData.DockingPlatform.name == "Platform1" && !(elevatorData.fuel == 100f))
        {
            flowchart.SendFungusMessage("Fuel");
            elevatorData.fuel = 100;
        }
    }

    private void Unload(LuggageData luggageData)
    {
        elevatorData.Luggage.Amount = 0;
    }

    public void Load(LuggageData newLuggageData)
    {
        elevatorData.Luggage.Amount = newLuggageData.Amount;
        elevatorData.Luggage.Destination = newLuggageData.Destination;
    }

    public void OnPointerDown(string button)
    {
        if (button == "Up")
        {
            _pointerState = PointerState.Up;
        }

        if (button == "Down")
        {
            _pointerState = PointerState.Down;
        }
    }

    //todo 指をスライドさせたときはupとdownどちらが先に呼ばれるのか?
    public void OnPointerUp(string button)
    {
        _pointerState = PointerState.None;
    }

    //called from elevatorCollider
    public void GetDamage(Collider other)
    {
        if (_damageCount > invincibleTime && data.BossShotDamage > 0)
        {
            //無敵時間がもう終わっている
            elevatorData.HP -= data.BossShotDamage;
            _audioSources[2].Play();
            elevatorController.RetDamage(other, elevatorData.HP);

            _damageCount = 0f;

            if (elevatorData.HP <= 0)
            {
                ElevatorDefeat();
            }
        }
    }

    private void UnDock()
    {
        elevatorData.IsDocked = false;
        elevatorData.DockingPlatform = null;
        // _rb.isKinematic = false;
        _rb.useGravity = true;
        elevatorData.IsDockingable = false;
    }

    private void Pause()
    {
        _rb.velocity = Vector3.zero;
        // _rb.isKinematic = true;
        _rb.useGravity = false;
        elevatorData.IsPaused = true;
        elevatorData.IsDockingable = false;
    }


    private void Resume()
    {
        // _rb.isKinematic = false;
        _rb.useGravity = true;
        elevatorData.IsDockingable = true;
        elevatorData.IsPaused = false;
    }

    private void OnBossDefeat()
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        gun.enabled = false;
    }

    private void ElevatorDefeat()
    {
        bossController.ElevatorDefeated();
        gun.enabled = false;
        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
        elevatorData.IsPaused = true;
    }

    private void OnRestartBossFight()
    {
        elevatorController.RetDamage(null, 100);
        gun.enabled = true;
        elevatorData.HP = 100;
        _rb.useGravity = true;
        elevatorData.IsPaused = false;
    }

    private void RunOutFuel()
    {
        elevatorController.RunOutFuel();
        Pause();
    }

    private void ReturnToGround()
    {
        StartCoroutine(Wait(1.5f, () =>
        {
            var ground = GameObject.Find("Platform1").transform.position;
            elevatorData.fuel = 100;
            Dock(ground);
        }));
    }

    private IEnumerator Wait(float waitSecond, Action action)
    {
        yield return new WaitForSeconds(waitSecond);
        action();
    }
}