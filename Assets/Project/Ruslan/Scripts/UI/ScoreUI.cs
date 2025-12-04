using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private string _levelId;
    [SerializeField] private TextMeshProUGUI _enemiesToKill;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _addedScore;
    [SerializeField] private Image _addedScoreImage;
    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private List<ScoreStar> _scoreStars;
    private int _currentIndex = 0;
    private bool _isCompleted => _currentIndex >= _scoreStars.Count;


    public void SetScoreManager(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    private void Start()
    {
        //_scoreManager.OnScoreChanged += UpdateScoreText;
        _scoreManager.OnScoreAdded += AddScore;
        _addedScoreImage.gameObject.SetActive(false);

    }

    public void SetStarsScore(string levelID, float first, float second, float third)
    {
        _levelId = levelID;
        _scoreStars[0].SetScoreToFill(first);
        _scoreStars[1].SetScoreToFill(second);
        _scoreStars[2].SetScoreToFill(third);
    }

    private void OnDisable()
    {
        //_scoreManager.OnScoreChanged -= UpdateScoreText;
        _scoreManager.OnScoreAdded -= AddScore;
    }
    public void UpdateEnemiesToKillText(string text)
    {
        _enemiesToKill.text = text;
    }
    //public void UpdateScoreText(float score)
    //{
    //    _score.text = score.ToString();
    //}

    private void AddScore(float score)
    {
        ShowAddedScore(score);
        FillImages(score);
    }

    private void ShowAddedScore(float score)
    {
        _addedScoreImage.gameObject.SetActive(true);
        _addedScore.text = score.ToString();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        _addedScoreImage.gameObject.SetActive(false);
    }

    private void FillImages(float amount)
    {
        if (_isCompleted)
            return;

        while (amount > 0 && _currentIndex < _scoreStars.Count)
        {
            var segment = _scoreStars[_currentIndex];

            float need = segment.ScoreToFill;

            // Если очков хватит, чтобы полностью заполнить сегмент
            if (amount >= need)
            {
                segment.AddScore(need);
                amount -= need;

                // Переходим к следующей звезде
                _currentIndex++;
                SaveStars();
            }
            else
            {
                // Заполняем частично и выходим
                segment.AddScore(amount);
                amount = 0;
            }
        }
    }

    private void SaveStars()
    {
        int stars = Mathf.Clamp(_currentIndex, 0, _scoreStars.Count);
        PlayerPrefs.SetInt(_levelId + "_Stars", stars);
        PlayerPrefs.Save();
    }
}
