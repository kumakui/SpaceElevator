using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCollider : MonoBehaviour
{
    public Elevator elevator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossShot"))
        {
            elevator.GetDamage(other);
        }
    }
}
