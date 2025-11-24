using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreChanged;
    [SerializeField] private int _screPerKill = 10;

    private int _totalScore;

    public void HandleHit(int hits)
    {
        int score = 0;
        score = hits * _screPerKill;
        AddScore(score);
    }

    public void AddScore(int digit)
    {
        _totalScore += digit;
        OnScoreChanged?.Invoke(_totalScore);
    }

    private void CheckHighScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (_totalScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", _totalScore);
        }
    }
}
