using UnityEngine;

[ExecuteAlways] // Работает в Edit Mode
public class ReflectingRay : MonoBehaviour
{
    public Transform origin;          // откуда летит сфера
    public float radius = 0.25f;      // радиус сферы
    public float maxDistance = 30f;   // максимальная длина луча
    public int maxBounces = 5;        // сколько раз может отскочить

    public Color lineColor = Color.yellow;
    public float hitSphereSize = 0.15f;

    private void OnDrawGizmos()
    {
        if (origin == null) return;

        Gizmos.color = lineColor;

        Vector3 pos = origin.position;
        Vector3 dir = origin.forward;

        for (int i = 0; i < maxBounces; i++)
        {
            if (Physics.SphereCast(pos, radius, dir, out RaycastHit hit, maxDistance))
            {
                // рисуем линию до касания
                Vector3 sphereCenterAtHit = hit.point - dir * radius;
                Gizmos.DrawLine(pos, sphereCenterAtHit);

                // показываем точку касания
                Gizmos.DrawWireSphere(sphereCenterAtHit, radius);

                // рассчитываем отражение
                dir = Vector3.Reflect(dir, hit.normal);

                // новая стартовая позиция
                pos = sphereCenterAtHit;
            }
            else
            {
                // пустота — просто рисуем прямую
                Gizmos.DrawLine(pos, pos + dir * maxDistance);
                break;
            }
        }
    }
}
