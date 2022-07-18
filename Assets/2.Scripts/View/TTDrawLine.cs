using System.Collections;
using System.Collections.Generic;
using TapTap;
using UnityEngine;

public class TTDrawLine : MonoBehaviour
{
    [SerializeField] KeyCode keyCode;
    private Transform m_point1;
    private Transform m_point2;
    private LineRenderer _lr;
    private PathInfo m_pathInfo;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        _lr = GetComponent<LineRenderer>();
        m_point1 = transform.Find("Point1").GetComponent<Transform>();
        m_point2 = transform.Find("Point2").GetComponent<Transform>();

        m_pathInfo = new PathInfo(m_point1, m_point2, keyCode);
        _lr.SetPosition(0, m_point1.position);
        _lr.SetPosition(1, m_point2.position);
    }

    public PathInfo GetPath()
    {
        return m_pathInfo;
    }
}
