using System.Collections.Generic;
using UnityEngine;

public class BounceRayTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform origin;
    [SerializeField] private int maxBounces = 5;
    [SerializeField] private float maxDistance = 100f;

    [Header("Sphere settings")]
    [SerializeField] private float sphereRadius = 0.25f;

    public void DrawTrajectory(Vector3 direction)
    {
        if (origin == null) return;

        Vector3 currentPos = origin.position;
        Vector3 currentDir = direction.normalized;

        List<Vector3> points = new List<Vector3>();
        points.Add(currentPos);

        for (int i = 0; i < maxBounces; i++)
        {
            // SphereCast вместо Raycast
            if (Physics.SphereCast(currentPos, sphereRadius, currentDir, out RaycastHit hit, maxDistance))
            {
                // ÷ентр сферы при касании должен быть смещЄн назад от точки касани€
                Vector3 sphereCenterHit = hit.point - currentDir * sphereRadius;

                points.Add(sphereCenterHit);

                // отражение от нормали
                currentDir = Vector3.Reflect(currentDir, hit.normal);

                // нова€ стартова€ точка Ч центр сферы после касани€
                currentPos = sphereCenterHit;
            }
            else
            {
                // пустота Ч рисуем пр€мую
                points.Add(currentPos + currentDir * maxDistance);
                break;
            }
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    public void Clear()
    {
        if (line != null)
            line.positionCount = 0;
    }
}
