using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [Header("Main Images")]
    [SerializeField] private Image firstImage;
    [SerializeField] private Image[] collageImages;

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

    void InitImages()
    {
        Color c1 = firstImage.color;
        c1.a = 1f;
        firstImage.color = c1;

        firstImage.transform.localScale = Vector3.one;

        foreach (var img in collageImages)
        {
            Color c = img.color;
            c.a = 0f;
            img.color = c;

            img.transform.localScale = Vector3.one * appearScale;
        }
    }

    IEnumerator SequenceRoutine()
    {
        // Первая картинка
        yield return StartCoroutine(FadeAndGrowFirst());

        // Остальные
        while (collageIndex < collageImages.Length)
        {
            skip = false; // чтобы Space переключал только текущий этап

            yield return StartCoroutine(ShowNextCollageImage(collageImages[collageIndex]));

            collageIndex++;
        }

        // В конце — грузим сцену
        SceneManager.LoadScene(_sceneToLoad);
    }

    IEnumerator FadeAndGrowFirst()
    {
        float t = 0f;

        Color c = firstImage.color;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.one * firstScale;

        while (t < fadeTime)
        {
            if (skip) break;

            t += Time.deltaTime;
            float p = t / fadeTime;

            c.a = Mathf.Lerp(1f, 0.5f, p);
            firstImage.color = c;

            firstImage.transform.localScale = Vector3.Lerp(startScale, endScale, p);

            yield return null;
        }
    }

    IEnumerator ShowNextCollageImage(Image img)
    {
        float t = 0f;
        Color c = img.color;

        Vector3 startScale = Vector3.one * appearScale;
        Vector3 endScale = Vector3.one;

        while (t < fadeTime)
        {
            if (skip) break;

            t += Time.deltaTime;
            float p = t / fadeTime;

            c.a = p;
            img.color = c;

            img.transform.localScale = Vector3.Lerp(startScale, endScale, p);

            yield return null;
        }

        c.a = 1f;
        img.color = c;
        img.transform.localScale = endScale;
    }
}
