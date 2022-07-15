using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]private Transform[] points;
    private LineRenderer _lr;
    

    private void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
            _lr.SetPosition(i, points[i].position);
    }
}
