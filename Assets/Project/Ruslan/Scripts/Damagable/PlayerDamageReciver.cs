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
    [SerializeField] private float _maxHits;
    [SerializeField] private float _minHits;
    [SerializeField] private float hits = 1;
    [SerializeField] private float currentHealth;
    [SerializeField] private float restoreTimer = 5;
    private float currentRestoreTimer;
    private Vignette vignetteSettings;
    private Coroutine changeCoroutine;

    void Start()
    {
        if (_globalVolume.profile.TryGet(out vignetteSettings))
        {
            hits = _minHits;
            currentHealth = hits / _maxHits;
            Debug.Log("Vignette found");
        }
        else
        {
            Debug.LogError("Vignette override not found in Volume!");
        }
    }

    private void Update()
    {
        if( hits > _minHits && currentRestoreTimer < restoreTimer)
        {
            currentRestoreTimer += Time.deltaTime;
            if (currentRestoreTimer >= restoreTimer)
            {
                currentRestoreTimer = restoreTimer;
                changeCoroutine = StartCoroutine(SmoothlyChangeVignette(0, smoothRestoreDuration));
                hits = _minHits;
                currentHealth = hits / _maxHits;
            }
        }
    }

    public void TakeDamage()
    {
        if (hits < _maxHits)
        {
            hits++;
            currentHealth = hits / _maxHits;
            changeCoroutine = StartCoroutine(SmoothlyChangeVignette(currentHealth, smoothChangeDuration));
        }
        currentRestoreTimer = 0;
    }

    IEnumerator SmoothlyChangeVignette(float targetValue, float changeDuration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < changeDuration)
        {
            float newIntensity = Mathf.Lerp(vignetteSettings.intensity.value, targetValue, elapsedTime / smoothChangeDuration);
            vignetteSettings.intensity.value = newIntensity;
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        vignetteSettings.intensity.value = targetValue;
    }
}
