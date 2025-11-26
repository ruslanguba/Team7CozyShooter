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

    [SerializeField] private float positionOffset = 50;
    [SerializeField] private float sizeOffset;

    private Vector2 leftOriginalSize;
    private Vector2 rightOriginalSize;

    private Vector3 leftOriginalPos;
    private Vector3 rightOriginalPos;


    private void Awake()
    {
        leftOriginalSize = leftImage.rectTransform.sizeDelta;
        rightOriginalSize = rightImage.rectTransform.sizeDelta;

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

        // Восстанавливаем размер и позицию невыбранного
        other.sizeDelta = isRight ? leftOriginalSize : rightOriginalSize;
        other.localPosition = isRight ? leftOriginalPos : rightOriginalPos;

        // Увеличиваем выбранный элемент и смещаем его
        selected.sizeDelta += new Vector2(100, 100);

        Vector3 newPos = selected.localPosition;
        newPos.y += 50;
        newPos.x += isRight ? -50 : 50;
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
