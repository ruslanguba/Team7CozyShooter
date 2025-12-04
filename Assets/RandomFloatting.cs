using System.Collections;
using UnityEngine;

public class RandomFloatting : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _radius = 5f;         // Радиус выбора точки
    [SerializeField] private float _minSpeed = 2f;
    [SerializeField] private float _maxSpeed = 4f;          // Скорость движения


    [Header("Height Limit")]
    [SerializeField] private float minY = -50f;             // Минимальная высота
    [SerializeField] private float maxY = 50f;             // Максимальная высота

    [Header("Pause")]
    public float stopTime = 2f;       // Время остановки

    private Vector3 targetPoint;      // Текущая цель
    private bool isMoving = true;
    private float _speed;

    private void Start()
    {
        PickNewPoint();
        StartCoroutine(MoveLoop());
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private IEnumerator MoveLoop()
    {
        while (true)
        {
            // Пока не долетел — летим
            while (Vector3.Distance(transform.position, targetPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPoint,
                    _speed * Time.deltaTime);

                yield return null;
            }

            // Долетел – остановка
            isMoving = false;
            yield return new WaitForSeconds(stopTime);

            // Новая точка
            PickNewPoint();
            isMoving = true;
        }
    }

    private void PickNewPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _radius;

        Vector3 randomOffset = Random.insideUnitSphere * _radius;
        targetPoint.y = Mathf.Clamp(targetPoint.y, minY, maxY);

        targetPoint = transform.position + randomOffset;
    }
}
