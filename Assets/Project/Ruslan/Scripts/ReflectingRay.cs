using UnityEngine;

[ExecuteAlways] // Работает в Edit Mode
public class ReflectingRay : MonoBehaviour
{
    [Header("Ray Settings")]
    public Vector3 direction = Vector3.forward;
    public float maxDistance = 100f;
    public int maxReflections = 50;

    [Header("Gizmo Settings")]
    public Color rayColor = Color.yellow;
    public Color hitColor = Color.red;
    public float hitSphereRadius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;

        Vector3 currentPos = transform.position;
        Vector3 currentDir = direction.normalized;

        for (int i = 0; i < maxReflections; i++)
        {
            if (Physics.Raycast(currentPos, currentDir, out RaycastHit hit, maxDistance))
            {
                // Рисуем линию
                Gizmos.DrawLine(currentPos, hit.point);

                // Показываем точку удара
                Gizmos.color = hitColor;
                Gizmos.DrawSphere(hit.point, hitSphereRadius);
                Gizmos.color = rayColor;

                // Отражение
                currentDir = Vector3.Reflect(currentDir, hit.normal);
                currentPos = hit.point;
            }
            else
            {
                // Если ничего не задел — рисуем луч в пустоту
                Gizmos.DrawLine(currentPos, currentPos + currentDir * maxDistance);
                break;
            }
        }
    }
}
