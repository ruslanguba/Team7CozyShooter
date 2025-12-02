using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerDamageReciver : MonoBehaviour
{
    [Header("Volume & Vignette")]
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private float smoothChangeDuration = 0.5f;
    [SerializeField] private float smoothRestoreDuration = 2f;

    [Header("Damage Settings")]
    [SerializeField] private float _maxHits = 5f;
    [SerializeField] private float _minHits = 0f;
    [SerializeField] private float restoreDelay = 5f;

    private float hits;
    private Vignette vignetteSettings;
    private Coroutine vignetteCoroutine;
    private float restoreTimer;

    private void Awake()
    {
        if (_globalVolume == null)
        {
            Debug.LogWarning("PlayerDamageReceiver: Global Volume is not assigned!");
            enabled = false;
            return;
        }

        if (!_globalVolume.profile.TryGet(out vignetteSettings))
        {
            Debug.LogWarning("PlayerDamageReceiver: Vignette override not found!");
            enabled = false;
            return;
        }

        hits = _minHits;
        vignetteSettings.intensity.value = 0f;
        restoreTimer = restoreDelay;
    }

    private void Update()
    {
        // Автоматическое восстановление после таймера
        if (hits > _minHits)
        {
            restoreTimer -= Time.deltaTime;
            if (restoreTimer <= 0f)
            {
                restoreTimer = restoreDelay;
                hits = _minHits;
                ChangeVignetteSmoothly(0f, smoothRestoreDuration);
            }
        }
    }

    public void TakeDamage()
    {
        if (vignetteSettings == null) return;

        hits = Mathf.Min(hits + 1f, _maxHits);
        float targetIntensity = hits / Mathf.Max(_maxHits, 0.0001f);

        ChangeVignetteSmoothly(targetIntensity, smoothChangeDuration);

        restoreTimer = restoreDelay;
    }

    private void ChangeVignetteSmoothly(float targetValue, float duration)
    {
        if (vignetteCoroutine != null)
            StopCoroutine(vignetteCoroutine);

        vignetteCoroutine = StartCoroutine(SmoothlyChangeVignette(targetValue, duration));
    }

    private IEnumerator SmoothlyChangeVignette(float targetValue, float duration)
    {
        if (vignetteSettings == null) yield break;

        float startValue = vignetteSettings.intensity.value;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vignetteSettings.intensity.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }

        vignetteSettings.intensity.value = targetValue;
    }
}
