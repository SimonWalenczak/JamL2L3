using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    public Transform Parent;
    private Vector3 pos;

    void Start()
    {
        pos = Parent.transform.InverseTransformPoint(transform.position);
    }
    void Update()
    {
        var newpos = Parent.transform.TransformPoint(pos);
        transform.position = newpos;
    }

}