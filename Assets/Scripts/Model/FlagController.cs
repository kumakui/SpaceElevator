using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private Flowchart _flowchart;

    public bool tutorial01 = false; //falseなら未実施．trueなら完了
    // Start is called before the first frame update
    void Start()
    {
        _flowchart = GetComponent<Flowchart>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
