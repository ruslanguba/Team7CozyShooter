using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerDamageReciver : MonoBehaviour
{
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private float intensityTarget = 1f;
    [SerializeField] private float smoothChangeDuration = 2f;
    [SerializeField] private float smoothRestoreDuration = 5f;
    [SerializeField] private float _maxHits = 5f;
    [SerializeField] private float _minHits = 1f;

    private float hits;
    private float currentHealth;
    private float restoreTimer = 5f;
    private float currentRestoreTimer;

    private Vignette vignetteSettings;
    private Coroutine changeCoroutine;

    private void Start()
    {
        if (_globalVolume == null)
        {
            Debug.LogWarning("PlayerDamageReciver: Global Volume is not assigned!");
            enabled = false;
            return;
        }

        if (!_globalVolume.profile.TryGet(out vignetteSettings))
        {
            Debug.LogWarning("PlayerDamageReciver: Vignette override not found in Volume profile!");
            enabled = false;
            return;
        }

        hits = _minHits;
        currentHealth = hits / Mathf.Max(_maxHits, 0.0001f);
        vignetteSettings.intensity.value = 0f;
    }

    private void Update()
    {
        if (hits > _minHits)
        {
            currentRestoreTimer += Time.deltaTime;
            if (currentRestoreTimer >= restoreTimer)
            {
                currentRestoreTimer = restoreTimer;
                hits = _minHits;
                currentHealth = hits / Mathf.Max(_maxHits, 0.0001f);

                if (vignetteSettings != null)
                {
                    if (changeCoroutine != null)
                        StopCoroutine(changeCoroutine);

                    changeCoroutine = StartCoroutine(SmoothlyChangeVignette(0f, smoothRestoreDuration));
                }
            }
        }
    }

    public void TakeDamage()
    {
        if (vignetteSettings == null) return;

        if (hits < _maxHits)
        {
            hits++;
            currentHealth = hits / Mathf.Max(_maxHits, 0.0001f);

            if (changeCoroutine != null)
                StopCoroutine(changeCoroutine);

            changeCoroutine = StartCoroutine(SmoothlyChangeVignette(currentHealth, smoothChangeDuration));
        }

        currentRestoreTimer = 0f;
    }

    private IEnumerator SmoothlyChangeVignette(float targetValue, float duration)
    {
        if (vignetteSettings == null) yield break;

        float startValue = vignetteSettings.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            vignetteSettings.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignetteSettings.intensity.value = targetValue;
    }
}
