using UnityEngine.UI;
using UnityEngine;

public class LVLButtonStars : MonoBehaviour
{
    [SerializeField] private string _levelId;
    [SerializeField] private Image[] _stars;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();

        LoadStars();
        UpdateButtonState();
    }

    public void LoadStars()
    {
        int stars = PlayerPrefs.GetInt(_levelId + "_Stars", 0);

        for (int i = 0; i < _stars.Length; i++)
            _stars[i].gameObject.SetActive(i < stars);
    }

    private void UpdateButtonState()
    {
        // Разрешаем первый уровень всегда
        if (_levelId == "Level1")
        {
            _button.interactable = true;
            return;
        }

        int completed = PlayerPrefs.GetInt(_levelId + "_Completed", 0);
        _button.interactable = completed == 1;
    }
}
