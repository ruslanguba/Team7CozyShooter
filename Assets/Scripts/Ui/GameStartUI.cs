using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [Header("Main Images")]
    [SerializeField] private Image firstImage;          // первая
    [SerializeField] private Image[] collageImages;     // остальные

    [Header("Settings")]
    [SerializeField] private float fadeTime = 1.2f;
    [SerializeField] private float scaleTime = 1.0f;
    [SerializeField] private float appearScale = 1.12f;
    [SerializeField] private float firstScale = 1.08f;

    private int collageIndex = 0;
    private bool skip = false;

    [SerializeField] private string _sceneToLoad;

    void Start()
    {
        InitImages();
        StartCoroutine(SequenceRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            skip = true;
    }

    // ---------------- initialization ------------------
    void InitImages()
    {
        // первая полностью видима
        Color c1 = firstImage.color;
        c1.a = 1f;
        firstImage.color = c1;
        firstImage.transform.localScale = Vector3.one;

        // остальные прозрачны и увеличены
        foreach (var img in collageImages)
        {
            Color c = img.color;
            c.a = 0f;
            img.color = c;

            img.transform.localScale = Vector3.one * appearScale;
        }
    }

    // ---------------- sequence ------------------
    IEnumerator SequenceRoutine()
    {
        // Запускаем затемнение и увеличение первой
        StartCoroutine(FadeAndGrowFirst());

        // запускаем показ остальных картинок
        while (collageIndex < collageImages.Length)
        {
            yield return StartCoroutine(ShowNextCollageImage(collageImages[collageIndex]));
            collageIndex++;
        }
    }

    // ---------------- first image animation ------------------
    IEnumerator FadeAndGrowFirst()
    {
        float t = 0f;

        Color c = firstImage.color;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.one * firstScale;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float p = t / fadeTime;

            // затемнение
            c.a = Mathf.Lerp(1f, 0.5f, p);
            firstImage.color = c;

            // увеличение
            firstImage.transform.localScale = Vector3.Lerp(startScale, endScale, p);

            yield return null;
        }
    }

    // ---------------- other images animation ------------------
    IEnumerator ShowNextCollageImage(Image img)
    {
        skip = false;

        float t = 0f;
        Color c = img.color;

        Vector3 startScale = Vector3.one * appearScale;
        Vector3 endScale = Vector3.one;

        // fade-in + scale-down
        while (t < fadeTime)
        {
            if (skip) break;

            t += Time.deltaTime;
            float p = t / fadeTime;

            // появление
            c.a = p;
            img.color = c;

            // уменьшение из 1.12 1.0
            img.transform.localScale = Vector3.Lerp(startScale, endScale, p);

            yield return null;
        }

        // на всякий случай выставляем финальные значения
        c.a = 1f;
        img.color = c;
        img.transform.localScale = endScale;
    }
}
