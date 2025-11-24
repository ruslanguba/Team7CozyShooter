using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemiesToKill;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private ScoreManager _scoreManager;

    public void SetScoreManager(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
        _scoreManager.OnScoreChanged += UpdateScoreText;
    }
    private void Start()
    {
        _scoreManager.OnScoreChanged += UpdateScoreText;
    }
    private void OnDisable()
    {
        _scoreManager.OnScoreChanged -= UpdateScoreText;
    }
    public void UpdateEnemiesToKillText(string text)
    {
        _enemiesToKill.text = text;
    }

    public void UpdateScoreText(int score)
    {
        _score.text = score.ToString();
    }
}
