using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBound : MonoBehaviour
{
    public Material ground;
    public Material space;
    public GameController gameController;


    private Vector3 _elevatorEnterPos;
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
        if (other.name == "Elevator")
        {
            _elevatorEnterPos = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.name == "Elevator")
        {
            if (other.transform.position.y > _elevatorEnterPos.y)
            {
                //境界を下から上に通過
                gameController.onRegionChange(GameController.Region.Space);

            }
            if (other.transform.position.y < _elevatorEnterPos.y)
            {
                //境界を上から下に通過
                gameController.onRegionChange(GameController.Region.Earth);
            }

        }
    }
}
