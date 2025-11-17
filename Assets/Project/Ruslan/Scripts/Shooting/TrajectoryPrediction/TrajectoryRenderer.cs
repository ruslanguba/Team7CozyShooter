using UnityEngine;
using System.Collections.Generic;

public class TrajectoryRenderer
{
    private readonly LineRenderer _lineRenderer;

    public TrajectoryRenderer(LineRenderer lineRenderer)
    {
        _lineRenderer = lineRenderer;
    }

    public void Render(List<Vector3> points)
    {
        if (_lineRenderer == null) return;

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }

    public void Clear()
    {
        if (_lineRenderer == null) return;
        _lineRenderer.positionCount = 0;
    }
}
