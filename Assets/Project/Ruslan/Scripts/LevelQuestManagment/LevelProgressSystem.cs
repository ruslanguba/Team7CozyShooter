using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressSystem : MonoBehaviour
{
    public event Action<int> OnEnemeyKilled;
    public event Action OnLevelCompleat;

    [SerializeField] private List<EnemyHealth> enemiesToKill;
    [SerializeField] private ScoreUI _scoreUI;
    int enemiesCount;

    private void OnEnable()
    {
        foreach (EnemyHealth health in enemiesToKill)
        {
            health.OnDeath += DetectKill;
        }
        enemiesCount = enemiesToKill.Count;
    }

    private void Start()
    {
        if (_scoreUI == null)
        {
            _scoreUI = FindFirstObjectByType<ScoreUI>();
        }
        _scoreUI.UpdateEnemiesToKillText(enemiesCount.ToString());
    }
    private void DetectKill()
    {
        enemiesCount--;
        OnEnemeyKilled?.Invoke(enemiesCount);
        CheckIfCompleat();
        _scoreUI.UpdateEnemiesToKillText(enemiesCount.ToString());
    }

    private void CheckIfCompleat()
    {
        if (enemiesCount == 0)
        {
            Debug.Log("LevelCompleat");
            OnLevelCompleat?.Invoke();
        }
    }
}
