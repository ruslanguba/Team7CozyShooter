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

    public void RoteteToPlayer(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }
}
