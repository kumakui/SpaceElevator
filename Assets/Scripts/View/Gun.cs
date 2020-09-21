using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Shot shotPrefab;
    public float shotSpeed;
    public float shotAngleRange;
    public float shotTimer;
    public int shotCount;
    public float shotInterval;

    private string shotTagName = "ElevatorShot";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        if(shotTimer < shotInterval) return;
        shotTimer = 0;

        shotN(8, 0, shotSpeed, 3);
        // shotNWay(0, shotAngleRange, shotSpeed, shotCount);
    }

    private void shotNWay(float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.position;
        var rot = Quaternion.Euler(0, 0, 0);

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                var angle = angleBase + angleRange * ((float) i / (count - 1) - 0.5f);
                var shot = Instantiate(shotPrefab, pos, rot);

                shot.Init(angle, speed, shotTagName);
            }
        }else if (count == 1)
        {
            var shot = Instantiate(shotPrefab, pos, rot);
            shot.Init(angleBase, speed, shotTagName);
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
                var initPosY = transform.position.y - (((float)count - 1) / 2) * shotDistance +
                              i * shotDistance;
                var initPos = new Vector3(pos.x, initPosY, pos.z);
                var shot = Instantiate(shotPrefab, initPos, rot);
                shot.Init(angle, speed, shotTagName);
            }
        }else if (count == 1)
        {
            var shot = Instantiate(shotPrefab, pos, rot);
            shot.Init(angle, speed, shotTagName);
        }
    }
}
