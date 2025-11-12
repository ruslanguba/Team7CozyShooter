using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _totalScoreText;
    [SerializeField] private Text _totalNightmareText;

    public static ScoreManager Instance;
    public static int TotalScore;
    public static int TotalNightmare;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        TotalScore = PlayerPrefs.GetInt("TotalScore", 0);
        _totalScoreText.text = TotalScore.ToString();

        TotalNightmare = PlayerPrefs.GetInt("TotalNightmare", 0);
        _totalNightmareText.text = TotalNightmare.ToString();

        //PlayerPrefs.SetInt("TotalScore", TotalScore);
        //_totalScoreText.text = PlayerPrefs.GetInt("TotalScore", 0).ToString();
        //PlayerPrefs.SetInt("TotalNightmare", TotalNightmare);
        //_totalNightmareText.text = PlayerPrefs.GetInt("TotalNightmare", 0).ToString();
    }

    public void HandleHit(int hits)
    {
        switch (hits)
        {
            case 1:
                AddScore(10); // Одно попадание даёт 10 очков
                break;
            case 2:
                AddScore(20); // Двух-кратное попадание (Комбо) даёт дополнительно 20 очков
                break;
            default:
                AddScore(20 * hits); // Больше трёх попаданий — бонус х3 за каждого дополнительного врага
                break;
        }
    }

    public void AddScore(int digit)
    {
        TotalScore += digit;

        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _totalScoreText.text = TotalScore.ToString();      
    }

    public void AddNightmare(int score)
    {
        TotalNightmare += score;

        PlayerPrefs.SetInt("TotalNightmare", TotalNightmare);
        _totalNightmareText.text = TotalNightmare.ToString();
    }
}
