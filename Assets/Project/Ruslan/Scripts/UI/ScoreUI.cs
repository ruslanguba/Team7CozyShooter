using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _enemiesToKill;

    public void UpdateCurrentScoreText(string text)
    {
        _currentScoreText.text = text;
    }

    public void UpdateEnemiesToKillText(string text)
    {
        _enemiesToKill.text = text;
    }

}
