using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillowUI : MonoBehaviour
{
    [SerializeField] private Image _iconPrefab;
    [SerializeField] private RectTransform _firstPosition;
    [SerializeField] private RectTransform _pillowsPanel;
    [SerializeField] private float _posOffset = 160f;
    [SerializeField] private float _alphaVisible = 255f;
    [SerializeField] private float _alphaHidden = 0f;

    private List<Image> _pillowIcons = new List<Image>();

    // Создаём иконки
    public void SpawnIcons(int count)
    {
        //ClearIcons();

        Vector3 startPos = _firstPosition.localPosition;

        for (int i = 0; i < count; i++)
        {
            Image icon = Instantiate(_iconPrefab, _pillowsPanel);
            icon.transform.localPosition = startPos + new Vector3(i * _posOffset, 0f, 0f);
            //SetAlpha(icon, _alphaVisible); // изначально скрыты
            
            _pillowIcons.Add(icon);
        }
    }

    // Показываем иконку с начала списка
    public void ShowPillow()
    {
        foreach (var icon in _pillowIcons)
        {
            if (icon.color.a < 0.1f)
            {
                SetAlpha(icon, _alphaVisible);
                break;
            }
        }
    }

    // Скрываем иконку с конца списка
    public void HidePillow()
    {
        for (int i = _pillowIcons.Count - 1; i >= 0; i--)
        {
            if (_pillowIcons[i].color.a > 0.9f)
            {
                SetAlpha(_pillowIcons[i], _alphaHidden);
                break;
            }
        }
    }

    private void SetAlpha(Image image, float alpha)
    {
        if (image == null) return;
        Color c = image.color;
        c.a = Mathf.Clamp01(alpha);
        image.color = c;
    }

    private void ClearIcons()
    {
        foreach (var icon in _pillowIcons)
            if (icon != null) Destroy(icon.gameObject);
        _pillowIcons.Clear();
    }
}
