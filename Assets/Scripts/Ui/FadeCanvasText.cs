using System.Collections;
using TMPro;
using UnityEngine;

public class FadeCanvasText : MonoBehaviour
{
    [Tooltip("Длительность исчезновения")]
    [SerializeField] private float _fadeDuration = 2;

    [Tooltip("Время задержки перед началом исчезновения")]
    [SerializeField] private float _startFade = 4;

    private TMP_Text text;
    private Color startColor;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        startColor = text.color;
    }

    void Start()
    {
        Invoke("StartFadeOut", _startFade);
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / _fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            yield return null;
        }

        gameObject.SetActive(false);
    }

}
