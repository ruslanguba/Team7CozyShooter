using Unity.VisualScripting;
using UnityEngine;

public class PropsMover : MonoBehaviour
{
    [SerializeField] private Transform _baloon;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotation = 0.5f;
    [SerializeField] private bool _isInterracted;

    [SerializeField] private float _floatingTime = 10;
    private int _pointIndex;
    private float _timer;

    private void Update()
    {
        if (!_isInterracted)
        {
            float distance = Vector3.Distance(_baloon.transform.position, _points[_pointIndex].position);
            if (distance < 0.2f)
            {
                SetNextPoint();
            }
            _baloon.transform.position = Vector3.MoveTowards(_baloon.transform.position, _points[_pointIndex].position, _speed * Time.deltaTime);
            _baloon.transform.Rotate(0, 0, _rotation);
        }
        else
        {
            _timer -= Time.deltaTime;
            _baloon.transform.position = Vector3.MoveTowards(_baloon.transform.position, _baloon.transform.position + Vector3.up, _speed * 2 * Time.deltaTime);
            if (_timer <= 0) 
            {
                SetNextPoint();
                _isInterracted = false;
            }
        }
    }

    private Vector3 SetNextPoint()
    {
        _pointIndex++;
        if (_pointIndex % _points.Length == 0)
        {
            _pointIndex = 0;
        }
        return _points[_pointIndex].position;
    }

    public void GetHit()
    {
        _timer = _floatingTime;
        _isInterracted = true;
    }
}
