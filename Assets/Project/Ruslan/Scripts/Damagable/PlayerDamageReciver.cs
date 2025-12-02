using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerDamageReciver : MonoBehaviour
{
    private PlayerKnockbackHandler knockbackHandler;

    [Header("Volume & Vignette")]
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private float smoothChangeDuration = 0.5f;
    [SerializeField] private float smoothRestoreDuration = 2f;
    [SerializeField] private float firstHitIntensity  = 0.6f;

    [Header("Damage Settings")]
    [SerializeField] private int _maxHits = 5;     // кол-во попаданий до полного эффекта
    [SerializeField] private int _minHits = 0;
    [SerializeField] private float restoreDelay = 5f;

    private int hits;
    private Vignette vignetteSettings;
    private Coroutine vignetteCoroutine;
    private float restoreTimer;

    private void Start()
    {
        if (_globalVolume == null)
        {
            Debug.LogWarning("Global Volume missing");
            enabled = false;
            return;
        }

        if (!_globalVolume.profile.TryGet(out vignetteSettings))
        {
            Debug.LogWarning("Vignette missing");
            enabled = false;
            return;
        }
        knockbackHandler = GetComponent<PlayerKnockbackHandler>();

        vignetteSettings.active = true;
        vignetteSettings.intensity.overrideState = true;
        vignetteSettings.smoothness.overrideState = true;

        hits = _minHits;
        vignetteSettings.intensity.value = 0f;

        // Fix first-frame evaluation
        vignetteSettings.intensity.value = 0.0001f;
        vignetteSettings.intensity.value = 0f;

        restoreTimer = restoreDelay;
    }

    private void Update()
    {
        // Автовосстановление после таймера
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

    public void TakeDamage(Vector3 hitDirection, float force)
    {
        if (vignetteSettings == null) return;

        hits = Mathf.Min(hits + 1, _maxHits);

        // Вычисляем интенсивность с учетом правил
        float targetIntensity = CalculateIntensity(hits, _maxHits);

        ChangeVignetteSmoothly(targetIntensity, smoothChangeDuration);

        if (knockbackHandler != null)
            knockbackHandler.AddKnockback(hitDirection, force);

        restoreTimer = restoreDelay;
    }

    private float CalculateIntensity(int current, int max)
    {
        if (current <= 1)
            return firstHitIntensity;

        if (max <= 1)
            return 1f;

        int steps = max - 1;
        int stepIndex = current - 1;

        float start = firstHitIntensity;
        float end = 1f;

        float t = Mathf.Clamp01((float)stepIndex / steps);

        return Mathf.Lerp(start, end, t);
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
