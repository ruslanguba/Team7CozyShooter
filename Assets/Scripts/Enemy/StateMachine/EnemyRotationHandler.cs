using UnityEngine;

public class EnemyRotationHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 vel = rb.linearVelocity;

        // если нет движения – не поворачиваем
        if (vel.sqrMagnitude < 0.01f)
            return;

        // игнорируем вертикаль
        vel.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(vel);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }
}
