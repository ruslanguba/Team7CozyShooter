using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerDamageVFX : MonoBehaviour
{
    [Header("Vignette")]
    [SerializeField] private Volume volume;
    [SerializeField] private float firstHitIntensity = 0.6f;
    [SerializeField] private float smoothChangeDuration = 0.5f;
    [SerializeField] private float smoothRestoreDuration = 2f;

    private Vignette vignette;
    private Coroutine routine;
    private PlayerDamageReciver health;

    private void Awake()
    {
        health = GetComponent<PlayerDamageReciver>();
    }

    private void Start()
    {
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
            vignette.intensity.overrideState = true;
        }

        //health.OnDamage += HandleDamage;
        //health.OnFullyRestored += HandleRestore;
    }

    private void HandleDamage(int hits, int maxHits, Vector3 dir, float force)
    {
        float target = CalculateIntensity(hits, maxHits);
        StartChange(target, smoothChangeDuration);
    }

    private void HandleRestore()
    {
        StartChange(0f, smoothRestoreDuration);
    }

    private float CalculateIntensity(int current, int max)
    {
        if (current <= 1)
            return firstHitIntensity;

        float t = (float)(current - 1) / (max - 1);
        return Mathf.Lerp(firstHitIntensity, 1f, t);
    }

    private void StartChange(float target, float duration)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(VignetteRoutine(target, duration));
    }

    private IEnumerator VignetteRoutine(float target, float duration)
    {
        float start = vignette.intensity.value;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }

        vignette.intensity.value = target;
    }
}
