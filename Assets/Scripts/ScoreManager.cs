using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public event Action<float> OnScoreChanged;
    public event Action<float> OnScoreAdded;
    [SerializeField] private float _screPerKill = 10;

    private float _totalScore;

    public void HandleHit(float hits)
    {
        float score = 0;
        score = hits * _screPerKill;
        AddScore(score);
    }

    public void AddScore(float Score)
    {
        _totalScore += Score;
        OnScoreAdded?.Invoke(Score);
        OnScoreChanged?.Invoke(_totalScore);
    }
}
