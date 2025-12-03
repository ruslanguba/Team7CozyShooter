using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSaveHandler : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private LevelProgressSystem levelprogress;
    private float _totalScore;

    private void OnEnable()
    {
        scoreManager.OnScoreChanged += ChangeScore;
        levelprogress.OnLevelCompleat += SaveHighScore;
    }

    private void OnDisable()
    {
        scoreManager.OnScoreChanged -= ChangeScore;
        levelprogress.OnLevelCompleat -= SaveHighScore;
    }

    private void ChangeScore(float score)
    {
        _totalScore = score;
    }

    public void SaveHighScore()
    {
        string key = GetLevelKey();

        float bestScore = PlayerPrefs.GetFloat(key, 0);

        if (_totalScore > bestScore)
        {
            PlayerPrefs.SetFloat(key, _totalScore);
            PlayerPrefs.Save();
        }
    }

    public float GetHighScore()
    {
        string key = GetLevelKey();
        return PlayerPrefs.GetFloat(key, 0);
    }

    private string GetLevelKey()
    {
        return "BestScore_LVL_" + SceneManager.GetActiveScene().buildIndex;
    }

}
