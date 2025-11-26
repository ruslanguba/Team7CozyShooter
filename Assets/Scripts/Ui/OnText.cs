using System.Collections;
using TMPro;
using UnityEngine;

public class OnText : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _fadeDuration = 4f;

    private TMP_Text text;
    private Color startColor;

    private void Awake()
    {
        text = _object.GetComponent<TMP_Text>();

        if (text != null)
        {
            startColor = text.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerContext playerContext))
        {
            _object.SetActive(true);
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        yield return new WaitForSeconds(elapsedTime);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / _fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            yield return null;
        }

        _object.SetActive(false);
    }
}