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
        _image.fillAmount = _currentScore / _scoreToFill;

        // «апуск анимации "поп"
        if (_popCoroutine != null)
            StopCoroutine(_popCoroutine);

        _popCoroutine = StartCoroutine(PopAnimation());
        _particle.Play();
    }
    private IEnumerator PopAnimation()
    {
        // ”величение
        float t = 0;
        Vector3 targetScale = _originalScale * _popScale;

        while (t < _popTime)
        {
            t += Time.deltaTime;
            float p = t / _popTime;
            transform.localScale = Vector3.Lerp(_originalScale, targetScale, p);
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
