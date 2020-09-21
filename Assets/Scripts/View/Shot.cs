using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += _velocity * Time.deltaTime;
    }

    public void Init(float angle, float speed, string tagName)
    {
        var direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0);

        _velocity = direction * speed;
        var shotAngle = transform.localEulerAngles;
        shotAngle.z = angle;
        transform.localEulerAngles = shotAngle;

        transform.tag = tagName;

        Destroy(gameObject, 2);
    }

    public void setTag(string name)
    {
        transform.tag = name;
    }
}
