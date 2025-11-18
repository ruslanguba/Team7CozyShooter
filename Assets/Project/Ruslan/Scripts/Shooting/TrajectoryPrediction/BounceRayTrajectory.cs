using System.Collections.Generic;
using UnityEngine;

public class BounceRayTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform origin;
    [SerializeField] private int maxBounces = 5;
    [SerializeField] private float maxDistance = 100f;

    public void DrawTrajectory(Vector3 direction)
    {
        Vector3 currentPos = origin.position;
        Vector3 currentDir = direction.normalized;

        List<Vector3> points = new List<Vector3>();
        points.Add(currentPos);

        for (int i = 0; i < maxBounces; i++)
        {
            if (Physics.Raycast(currentPos, currentDir, out RaycastHit hit, maxDistance))
            {
                points.Add(hit.point);

                // отражение как у сферы
                currentDir = Vector3.Reflect(currentDir, hit.normal);

                currentPos = hit.point;
            }
            else
            {
                // дальше пустота
                points.Add(currentPos + currentDir * maxDistance);
                break;
            }
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    public void Clear()
    {
        line.positionCount = 0;
    }
}
