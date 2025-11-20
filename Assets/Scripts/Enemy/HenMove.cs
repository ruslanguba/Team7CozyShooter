using UnityEngine;

public class HenMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _timeToReachSpeed = 2;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 2;

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.GetPlayerTransform) return;

        Vector3 toPlayer = (PlayerManager.Instance.GetPlayerTransform.position - transform.position).normalized;
        Vector3 force = _rb.mass * (toPlayer * _speed - _rb.linearVelocity) / _timeToReachSpeed;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        _rb.AddForce(force);
    }
}
