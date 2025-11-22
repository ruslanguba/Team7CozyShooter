using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemiesToKill;
    [SerializeField] private TextMeshProUGUI _score;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
    }
    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
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
