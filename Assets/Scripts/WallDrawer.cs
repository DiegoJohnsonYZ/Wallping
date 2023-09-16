using System;
using UnityEngine;

public class WallDrawer : MonoBehaviour
{
    [SerializeField] private float unitSize = 4f;
    [SerializeField, Range(0f, 5f)] private float lineWidth = 0.2f;

    private LineRenderer _lineRenderer;

    private void OnValidate()
    {
        GetMainComponents();
    }

    private void Awake()
    {
        GetMainComponents();
    }

    private void GetMainComponents()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }

    public void DeleteAllLinePoints()
    {
        //_lineRenderer.
    }
}