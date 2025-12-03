using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private float _scoreToAdd = 1f;
    public float ScoreToAdd => _scoreToAdd;

    [Header("Animations")]
    [SerializeField] private float _upScaleTime = 0.3f;
    [SerializeField] private float _hideScaleTime = 0.5f;
    [SerializeField] private Transform _targetPoint;

    private bool _isCollected;
    private Vector3 _originalScale;
    private Collider _collider;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!_isCollected)
            transform.Rotate(0, 1f, 0f);
    }

    public virtual void Collect(Transform target)
    {
        if (_isCollected) return;
        _isCollected = true;

        _collider.enabled = false;
        _targetPoint = target;
        StartCoroutine(AnimateCollect());
    }

    private IEnumerator AnimateCollect()
    {
        // Увеличение
        float t = 0;
        Vector3 bigScale = _originalScale * 1.3f;

        while (t < _upScaleTime)
        {
            t += Time.deltaTime;
            float p = t / _upScaleTime;
            transform.localScale = Vector3.Lerp(_originalScale, bigScale, p);
            yield return null;
        }

        // Полёт + уменьшение
        t = 0;
        Vector3 startPos = transform.position;

        while (t < _hideScaleTime)
        {
            t += Time.deltaTime;
            float p = t / _hideScaleTime;

            // движение к цели
            if (_targetPoint != null)
                transform.position = Vector3.Lerp(startPos, _targetPoint.position, p);

            // уменьшение
            transform.localScale = Vector3.Lerp(bigScale, Vector3.zero, p);

            yield return null;
        }

        // Уничтожаем
        Destroy(gameObject);
    }
}
