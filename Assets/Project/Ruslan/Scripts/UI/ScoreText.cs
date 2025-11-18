using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _currentScoreText;
    [SerializeField] private TextMeshPro _enemiesToKill;

    public void UpdateCurrentScoreText(string text)
    {
        _currentScoreText.text = text;
    }

    public void UpdateEnemiesToKillText(string text)
    {
        _enemiesToKill.text = text;
    }

}
