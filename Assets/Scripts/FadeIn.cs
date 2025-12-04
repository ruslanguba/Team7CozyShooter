using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float _fadeDuration = 3f;
    [SerializeField] private float _showDuration = 2f;

    private Color startColor;

    private void Awake()
    {
        if (image != null)
        {
            startColor = image.color;
        }
    }

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        yield return new WaitForSeconds(_showDuration);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / _fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            yield return null;
        }

        image.gameObject.SetActive(false);
    }
}
