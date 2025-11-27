using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Image panel;

    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;

    [SerializeField] private Image leftTextBackground;
    [SerializeField] private Image rightTextBackground;

    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;

    private float positionOffset;
    [SerializeField] private float sizeOffset;

    private Vector2 originalSize;

    private Vector3 leftOriginalPos;
    private Vector3 rightOriginalPos;


    private void Awake()
    {
        originalSize = leftImage.rectTransform.sizeDelta;

        leftOriginalPos = leftImage.rectTransform.localPosition;
        rightOriginalPos = rightImage.rectTransform.localPosition;

        panel.gameObject.SetActive(false);
    }

    public void SetLeftImage(Sprite sprite)
    {
        leftImage.sprite = sprite;
    }

    public void ShowNexSentence(bool isPluh, string text)
    {
        if (isPluh)
        {
            rightText.text = text;
            leftTextBackground.gameObject.SetActive(false);
            rightTextBackground.gameObject.SetActive(true);
        }
        else
        {
            leftText.text = text;
            leftTextBackground.gameObject.SetActive(true);
            rightTextBackground.gameObject.SetActive(false);
        }
        ChangeIconsSize(isPluh);
    }

    private void ChangeIconsSize(bool isRight)
    {

        // Определяем выбранный и другой элементы
        RectTransform selected = isRight ? rightImage.rectTransform : leftImage.rectTransform;
        RectTransform other = isRight ? leftImage.rectTransform : rightImage.rectTransform;

        other.sizeDelta = originalSize;
        selected.sizeDelta = originalSize;

        leftImage.rectTransform.localPosition = leftOriginalPos;
        rightImage.rectTransform.localPosition= rightOriginalPos;

        // Восстанавливаем размер и позицию невыбранного
        other.sizeDelta = originalSize;

        // Увеличиваем выбранный элемент и смещаем его
        selected.sizeDelta += Vector2.one * sizeOffset;
        positionOffset = sizeOffset / 2;
        Vector3 newPos = selected.localPosition;
        newPos.y += positionOffset;
        newPos.x += isRight ? -positionOffset : positionOffset;
        selected.localPosition = newPos;
    }

    public void OnNextButtonClick()
    {
        DialogueManager.Instance.DisplayNextSentence();
    }

    public void ShowPanel()
    {
        panel.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        panel.gameObject.SetActive(false);
    }

}
