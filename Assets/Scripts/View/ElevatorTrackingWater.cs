using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrackingWater : MonoBehaviour
{
    public ElevatorController elevatorController;
    public GameController gameController;
    public Vector3 upperBound;
    public Vector3 lowerBound;

    private float _spaceBoundHeight = 0f;
    private Vector3 _initialPos;
    private GameController.Region _region;

    // Start is called before the first frame update
    void Start()
    {
        elevatorController.elevatorPosChangeAction += OnElevatorPosChange;
        gameController.onRegionChangeAction += OnRegionChange;
        _spaceBoundHeight = GameObject.Find("SpaceBound").transform.position.y;

        _initialPos = transform.localPosition;
        _region = GameController.Region.Earth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnElevatorPosChange(Vector3 pos)
    {
        if (_region == GameController.Region.Earth)
        {
            var y = -((upperBound.y - lowerBound.y) / _spaceBoundHeight) * pos.y + upperBound.y;
            transform.localPosition = new Vector3(0, y, 498);
        }

    }

    private void OnRegionChange(GameController.Region region)
    {
        _region = region;
        if (region == GameController.Region.Space)
        {
            //宇宙空間に入ったら水を見えなくする
            transform.localPosition = new Vector3(0, -100, 468);
        }
    }
}
