using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public GameObject explosionParticle;
    public BossController bossController;
    public Data data;
    public float speed;
    public int HP;

    //弾
    public Shot normalShotPrefab;
    public Shot strongShotPrefab;
    public int shotCount;
    public float shotSpeed;
    public float shotAngleRange;
    public Vector3 shotPosOffset;
    public float attackTimer;
    public float normalAttackInterval; //通常攻撃の頻度[s]
    public float strongAttackIntervalCount; //強攻撃の頻度．通常攻撃のN回に一回が強攻撃になる
    public float invincibleTime; //被弾後の無敵時間
    public AudioSource entryAudioSource;
    public AudioSource damageAudioSource;

    private BossState _state;
    private Animator _animator;
    private CharacterController _characterController;
    private ElevatorData _elevatorData;
    private BoxCollider _elevatorCollider;
    private string _shotTagName = "BossShot";
    private int _normalAttackCount = 0; //通常攻撃を何回行ったか．強攻撃するたびにリセット

    private float _damageCount = 0f; //前回ダメージを受けてからの経過時間
    // private bool _attacking = false;
    // private int shotsCount;

    private WanderTarget
        _wanderTarget =
            WanderTarget.UpperBound; //ウロウロするときに，aroudelevatorcolliderの上下どちらの端を目標にするか

    public enum BossState
    {
        wait,
        chase,
        Wander
    }

    private enum WanderTarget
    {
        UpperBound,
        LowerBound
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _elevatorCollider = GameObject.Find("AroundElevatorCollider").GetComponent<BoxCollider>();
        entryAudioSource = GetComponent<AudioSource>();

        bossController.onElevatorRefreshAction += OnElevatorRefresh;
        bossController.onBossDefeatAction += Defeated;

        transform.position = bossController.EntryObject.transform.position;
        _state = BossState.wait;
        _animator.SetTrigger("Scream");
        entryAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == BossState.chase)
        {
            var diff = _elevatorData.Position.y - transform.position.y;
            speed = (0.6f * Mathf.Abs(diff)) + 200;
            if (speed > 1000)
            {
                speed = 1000f;
            }

            //移動先目標の座標．fly float中はドラゴンが少し浮いている分，目標を低めに設定．
            var targetPos = _elevatorData.Position;
            targetPos.y -= 70f;

            var direction = (targetPos - transform.position).normalized;
            direction.x = 0;
            direction.z = 0;

            _characterController.Move(direction * (speed * Time.deltaTime));
            // transform.position += direction * (speed * Time.deltaTime);
        }

        if (_state == BossState.Wander)
        {
            speed = 80;
            var target = Vector3.zero;
            var diff = new Vector3(0, _elevatorData.Position.y - transform.position.y, 0);

            if (_wanderTarget == WanderTarget.UpperBound)
            {
                target = Vector3.up;
            }

            if (_wanderTarget == WanderTarget.LowerBound)
            {
                target = Vector3.down;
            }

            _characterController.Move(target * (speed * Time.deltaTime));
        }

        _damageCount += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "AroundElevatorCollider")
        {
            SetState(BossState.Wander);
            if (transform.position.y > _elevatorData.Position.y)
            {
                //上からエレベーターに接近
                _wanderTarget = WanderTarget.LowerBound;
            }

            if (transform.position.y < _elevatorData.Position.y)
            {
                //下からエレベーターに接近
                _wanderTarget = WanderTarget.UpperBound;
            }
        }

        if (other.transform.CompareTag("ElevatorShot"))
        {
            Debug.Log("enter shot");
            GetDamage(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        if (other.transform.name == "AroundElevatorCollider")
        {
            SetState(BossState.chase);
        }
    }

    public void OnFightStart()
    {
        SetState(BossState.chase);
        _animator.SetBool("Fly", true);
        _characterController.center = new Vector3(0, 4.9f, 0);
        attackTimer = 0f; //戦闘開始時に攻撃タイマーをリセット．戦闘開始直後に攻撃を喰らわないように．

        StartAttack();
    }

    private void StartAttack()
    {
        _normalAttackCount = 0;
        //攻撃用のタイマーを開始
        //通常攻撃
        StartCoroutine(Loop(normalAttackInterval,
            () =>
            {
                _normalAttackCount++;

                if (_normalAttackCount > strongAttackIntervalCount)
                {
                    //strongAttackIntervalCountに1回だけ強攻撃
                    _animator.SetTrigger("Fire");
                    _normalAttackCount = 0;
                    StopAllCoroutines();
                }
                else
                {
                    shotNWay(180, shotAngleRange, shotSpeed, shotCount);
                }
            }));
    }


    private void SetState(BossState state)
    {
        _state = state;
    }

    private void OnElevatorRefresh(ElevatorData elevatorData)
    {
        _elevatorData = elevatorData;
    }

    private void shotNWay(float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.position + shotPosOffset;
        var rot = Quaternion.Euler(0, 0, 0);

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                var angle = angleBase + angleRange * ((float) i / (count - 1) - 0.5f);
                var shot = Instantiate(normalShotPrefab, pos, rot);

                shot.Init(angle, speed, _shotTagName);
            }
        }
        else if (count == 1)
        {
            var shot = Instantiate(normalShotPrefab, pos, rot);
            shot.Init(angleBase, speed, _shotTagName);
        }
    }

    private void shotTrackingNWay(float angleBase, float angleRange, float speed, int count,
        Vector3 targetPos)
    {
        var pos = transform.position + shotPosOffset;
        var rot = Quaternion.identity;
        var trackingAngle = 0f;

        if (targetPos == null)
        {
            rot = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            var diff = (targetPos - shotPosOffset) - transform.position;
            trackingAngle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) + 180f;
        }

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                var angle = angleBase + angleRange * ((float) i / (count - 1) - 0.5f);
                var shot = Instantiate(strongShotPrefab, pos, rot);

                shot.Init(angle + trackingAngle, speed, _shotTagName);
            }
        }
        else if (count == 1)
        {
            var shot = Instantiate(strongShotPrefab, pos, rot);
            shot.Init(angleBase + trackingAngle, speed, _shotTagName);
        }
    }

    private void shotN(float shotDistance, float angle, float speed, int count)
    {
        var pos = transform.position;
        var rot = Quaternion.Euler(0, 0, 0);

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                var initPosY = transform.position.y - (((float) count - 1) / 2) * shotDistance +
                               i * shotDistance;
                var initPos = new Vector3(pos.x, initPosY, pos.z);
                var shot = Instantiate(normalShotPrefab, initPos, rot);
                shot.Init(angle, speed, _shotTagName);
            }
        }
        else if (count == 1)
        {
            var shot = Instantiate(normalShotPrefab, pos, rot);
            shot.Init(angle, speed, _shotTagName);
        }
    }

    private void GetDamage(Collider other)
    {
        if (_damageCount > invincibleTime && data.GunDamage > 0)
        {
            var initialPos = transform.position;
            HP -= data.GunDamage;
            damageAudioSource.Play();
            _animator.SetTrigger("GetHit");
            //ダメージアニメーション中に攻撃するのはおかしいので，攻撃を中断してから再開．
            StopAllCoroutines();
            StartAttack();

            bossController.getDamage(other, transform.position);
            _damageCount = 0f;

            transform.position = new Vector3(120, transform.position.y, transform.position.z);
        }
    }

    //強攻撃アニメーションの攻撃タイミングで呼ばれる
    private void FireballShootEvent()
    {
        shotTrackingNWay(180, shotAngleRange, shotSpeed, shotCount, _elevatorData.Position);
        StartAttack();
    }

    private void Defeated()
    {
        _characterController.enabled = false;
        _animator.SetTrigger("Die");
    }

    //死亡モーション終了時
    private void DieAnimFinish()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Loop(float loopSecond, Action action)
    {
        while (true)
        {
            yield return new WaitForSeconds(loopSecond);
            action();
        }
    }
}