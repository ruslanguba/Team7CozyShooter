using UnityEngine;

public class EnemiesKillCounter : MonoBehaviour
{
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private LevelProgressSystem _levelProgressSystem;

    private void OnEnable()
    {
        if(_levelProgressSystem == null)
            _levelProgressSystem = FindAnyObjectByType<LevelProgressSystem>();
        _levelProgressSystem.OnEnemeyKilled += UpdateUI;
    }

    private void OnDisable()
    {
        _levelProgressSystem.OnEnemeyKilled -= UpdateUI;
    }

    private void UpdateUI(int enemiesLeft)
    { 
        _scoreUI.UpdateEnemiesToKillText(enemiesLeft.ToString());
    }
}
