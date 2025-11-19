using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _rotationSpeed = 2;
    private int currentPointIndex;
    private bool isHidden = false;

    void Start()
    {
        if (_points.Length > 0)
        {
            transform.position = _points[currentPointIndex].position;
        }
    }

    void Update()
    {
        if (!isHidden)
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        Vector3 direction = _points[currentPointIndex].position - transform.position;

        if (direction.magnitude <= 0.5f)
        {
            if (currentPointIndex + 1 < _points.Length)
            {
                currentPointIndex++;
            }

            else
            {
                StopMoving();
                return;
            }
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void StopMoving()
    {
        transform.Translate(Vector3.zero);
        enabled = false;
    }

    private void HideCharacter()
    {
        gameObject.SetActive(false);
        isHidden = true;
    }

    public void ContinueMovement()
    {
        gameObject.SetActive(true);
        isHidden = false;
    }
}
