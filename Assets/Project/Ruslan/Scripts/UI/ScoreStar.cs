using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class ScoreStar : MonoBehaviour
{
    [SerializeField] private float _scoreToFill;
    [SerializeField] private float _popScale = 1.3f;      // насколько увеличиваетс€
    [SerializeField] private float _popTime = 0.2f;       // врем€ увеличени€ и уменьшени€
    [SerializeField] private ParticleSystem _particle;

    private Image _image;
    private float _currentScore;
    private Vector3 _originalScale;
    private Coroutine _popCoroutine;
    private float _targetFillAmount;

    public float ScoreToFill => _scoreToFill - _currentScore;
    public bool IsFull => _currentScore >= _scoreToFill;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _originalScale = transform.localScale;
    }

    public void AddScore(float score)
    {
        _currentScore += score;
        _currentScore = Mathf.Min(_currentScore, _scoreToFill);
        _targetFillAmount = _currentScore / _scoreToFill;

        if( _currentScore >= _scoreToFill )
            _particle.Play();
        // «апуск анимации "поп"
        if (_popCoroutine != null)
            StopCoroutine(_popCoroutine);

        _popCoroutine = StartCoroutine(PopAnimation());
    }
    private IEnumerator PopAnimation()
    {
        // ”величение
        float t = 0;
        Vector3 targetScale = _originalScale * _popScale;
        float startFill = _image.fillAmount;

        while (t < _popTime)
        {
            t += Time.deltaTime;
            float p = t / _popTime;
            transform.localScale = Vector3.Lerp(_originalScale, targetScale, p);
            _image.fillAmount = Mathf.Lerp(startFill, _targetFillAmount, p);
            yield return null;
        }

        // ¬озврат к исходному размеру
        t = 0;
        while (t < _popTime)
        {
            t += Time.deltaTime;
            float p = t / _popTime;
            transform.localScale = Vector3.Lerp(targetScale, _originalScale, p);
            yield return null;
        }

        transform.localScale = _originalScale;
        _popCoroutine = null;
        
    }
}
