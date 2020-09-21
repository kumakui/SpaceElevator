using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem _partial;

    // Start is called before the first frame update
    void Start()
    {
        _partial = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_partial.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
