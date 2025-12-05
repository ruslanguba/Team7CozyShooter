using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintDestroyOnTime : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _fadeDuration = 1;
    [SerializeField] private float _showDuration = 2;

    private Image text;
    private Color startColor;

    private List<Graphic> graphicsToFade;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        graphicsToFade = GetGraphicsFromChildren(_object);

        foreach (Graphic graphic in graphicsToFade)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 1f);
        }
    }

    private List<Graphic> GetGraphicsFromChildren(GameObject root)
    {
        var result = new List<Graphic>();
        foreach (Graphic g in root.GetComponentsInChildren<Graphic>(true))
        {
            result.Add(g);
        }
        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerContext playerContext))
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            foreach (Graphic graphic in graphicsToFade)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 1f);
            }
            _object.SetActive(true);
            fadeRoutine = StartCoroutine(FadeOutAllGraphics());

        }
    }

    private IEnumerator FadeOutAllGraphics()
    {
        yield return new WaitForSeconds(_showDuration);
        float elapsedTime = 0;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            foreach (var graphic in graphicsToFade)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration);
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
