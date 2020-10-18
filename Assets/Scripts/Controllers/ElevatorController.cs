using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour
{
    public PlatformController platformController;
    public TutorialController tutorialController;
    public BossController bossController;
    public float spaceBoundHeight;
    public Flowchart flowchart;

    public UnityAction<float> elevatorSpeedChangeAction;
    public UnityAction<float> elevatorDockingProgressAction;
    public UnityAction<Vector3> dockAction;
    public UnityAction<float> elevatorTempChangeAction;
    public UnityAction<int, GameObject> luggageInfoUpdateAction;
    public UnityAction<String, float> keyPushAction;
    public UnityAction elevatorPauseAction;
    public UnityAction elevatorResumeAction;
    public UnityAction<int> onElevatorDamageAction;
    public UnityAction<Vector3> elevatorPosChangeAction;
    public UnityAction<float> elevatorFuelChangeAction;
    public UnityAction runOutFuelAction;
    public UnityAction ReturnToGroundAction;

    private ElevatorData _elevatorData;
    private float _progress = 0f;
    private float _dockingDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Refresh(ElevatorData data)
    {
        _elevatorData = data;

        //速度表示の更新
        elevatorSpeedChangeAction.Invoke(data.Speed);
        //エンジン温度の更新
        elevatorTempChangeAction.Invoke(data.Temp);
        //荷物量と目的地の更新
        luggageInfoUpdateAction.Invoke(data.Luggage.Amount, data.Luggage.Destination);
        //座標更新
        // elevatorPosChangeAction.Invoke(data.Position);
        //燃料更新
        elevatorFuelChangeAction.Invoke(data.fuel);

        bossController.OnElevatorRefresh(data);

        //ドッキング時プログレスバー更新
        if (data.IsDockingProgress && !data.IsDocked)
        {
            _progress += Time.deltaTime;
            elevatorDockingProgressAction.Invoke(_progress / _dockingDuration);

            if (_progress >= _dockingDuration)
            {
                _progress = 0f;
                elevatorDockingProgressAction.Invoke(_progress / _dockingDuration);
                Dock();
            }
        }
        else
        {
            _progress = 0f;
            elevatorDockingProgressAction.Invoke(_progress / _dockingDuration);
        }
    }

    //チュートリアル開始判定用
    public void onEnterCollision(Collider collider)
    {
        tutorialController.onEnterCollision(collider);
    }

    public void PauseElevator()
    {
        elevatorPauseAction.Invoke();
    }

    public void ResumeElevator()
    {
        elevatorResumeAction.Invoke();
    }
    
    private void Dock()
    {
        Vector3 DockingPos =
            _elevatorData.DockingPlatform.transform.Find("PlatformCollider").position;
        elevatorDockingProgressAction.Invoke(0);

        dockAction.Invoke(DockingPos);
        platformController.Dock(_elevatorData.DockingPlatform, _elevatorData.Luggage);
    }

    public void OnKeyStateChanged(String key)
    {
        if (_elevatorData != null)
        {
            keyPushAction.Invoke(key, _elevatorData.Temp);
        }
    }

    public void OnBossStart()
    {
        _elevatorData.InBattle = true;
    }

    public void RetDamage(Collider other, int HP)
    {
        onElevatorDamageAction.Invoke(HP);
    }

    public void RunOutFuel()
    {
        flowchart.SendFungusMessage("RunOutFuel");
    }

    //燃料切れ後に地上に戻る
    public void ReturnToGround()
    {
        ReturnToGroundAction.Invoke();
        StartCoroutine(Wait(4f, () =>
        {
            flowchart.SendFungusMessage("ReturnToGround");
        }));

    }

    private IEnumerator Wait(float waitSecond, Action action)
    {
        yield return new WaitForSeconds(waitSecond);
        action();
    }
}
