using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public LineRenderer line;
    public void DrawTrajectory(List<Vector3> points)
    {
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}
