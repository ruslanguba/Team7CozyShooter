using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _rotationSpeed = 2;
    private int currentPointIndex = 0;

    void Start()
    {
        if (_points.Length > 0)
        {
            transform.position = _points[currentPointIndex].position;
        }
    }

    void Update()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        Vector3 direction = _points[currentPointIndex].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        if (direction.magnitude <= 0.5f)
        {
            currentPointIndex++;

            if (currentPointIndex >= _points.Length)
            {
                currentPointIndex = 0;
            }
        }
    }
}
