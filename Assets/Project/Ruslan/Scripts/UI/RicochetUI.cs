using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RicochetUI : MonoBehaviour
{
    [SerializeField] private Image[] _multiplierImages;
    [SerializeField] protected float _yOffset = 25;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        for (int i = 0; i < _multiplierImages.Length; i++)
        {
            _multiplierImages[i].gameObject.SetActive(false);
        }
    }


    public void ShowRicochet(int multiplier, Vector3 worldPos)
    {
        int index = multiplier - 2;
        if (index < 0 || index >= _multiplierImages.Length)
            return;

        Image img = _multiplierImages[index];

        // Включаем
        img.gameObject.SetActive(true);

        // Переводим world - screen
        Vector3 screenPos = _camera.WorldToScreenPoint(worldPos);
        Vector3 offsetPos = new Vector3(screenPos.x, screenPos.y + _yOffset, screenPos.z);
        // Ставим UI-элемент туда
        img.rectTransform.position = offsetPos;

        // Можно добавить эффект исчезновения
        StartCoroutine(FadeOut(img));
    }

    private IEnumerator FadeOut(Image img)
    {
        yield return new WaitForSeconds(0.5f);
        img.gameObject.SetActive(false);
    }
}
