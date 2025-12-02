using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnText : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _fadeDuration = 4f;

    private Image text;
    private Color startColor;

    private List<Graphic> graphicsToFade;

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
        if (other.gameObject.TryGetComponent<PlayerContext>(out PlayerContext playerContext))
        {
            _object.SetActive(true);
            StartCoroutine(FadeOutAllGraphics());
        }
    }

    private IEnumerator FadeOutAllGraphics()
    {
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
        
        _object.SetActive(false);
    }

    //private void Awake()
    //{
    //    text = _object.GetComponent<Image>();

    //    if (text != null)
    //    {
    //        startColor = text.color;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out PlayerContext playerContext))
    //    {
    //        _object.SetActive(true);
    //        StartCoroutine(FadeOut());
    //    }
    //}

    //private IEnumerator FadeOut()
    //{
    //    float elapsedTime = 0;
    //    yield return new WaitForSeconds(elapsedTime);
    //    while (elapsedTime < _fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;

    //        float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / _fadeDuration);
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

    //        yield return null;
    //    }

    //    _object.SetActive(false);
    //}
}