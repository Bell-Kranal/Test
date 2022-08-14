using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRenderController : MonoBehaviour
{
    private LineRenderer _lineRender;
    private List<Vector3> _travelPoints;

    private void Start()
    {
        _lineRender     = GetComponent<LineRenderer>();
        _travelPoints   = new List<Vector3>();

        AddDefaultPoint();
    }

    private void AddDefaultPoint()
    {
        _lineRender.positionCount++;

        _travelPoints.Add(Vector3.zero);
        _lineRender.SetPosition(0, Vector3.zero);
    }

    public void TryAddPoint(Vector3 point)
    {
        if (_lineRender.positionCount <= _travelPoints.Count)
        {
            _lineRender.positionCount = _travelPoints.Count + 1;
        }

        AddPoint(point);
    }

    public void RemoveFirstPoint()
    {
        int size = _travelPoints.Count;
        for(int i = 1; i < size; i++)
        {
            _lineRender.SetPosition(i - 1, _lineRender.GetPosition(i));
        }

        _travelPoints.RemoveAt(0);
        _lineRender.positionCount--;
    }

    private void AddPoint(Vector3 point)
    {
        _lineRender.SetPosition(_travelPoints.Count, point);
        _travelPoints.Add(point);
    }

    public void DisableLineRenderer()
    {
        _lineRender.enabled = false;
        this.enabled = false;
    }
}
