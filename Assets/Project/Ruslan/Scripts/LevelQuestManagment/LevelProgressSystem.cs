using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelProgressSystem : MonoBehaviour
{
    public event Action<int> OnEnemeyKilled;
    public event Action OnLevelCompleat;

    [SerializeField] private Canvas _scoreCanvasPrefab;
    [SerializeField] private List<EnemyHealth> enemiesToKill;
    [SerializeField] private float _distance = 8;
    private SceneTransitionTrigger _portal;

    private ScoreUI _scoreUI;
    private EnemiesHandler _enemyHandler;
    private ScoreManager _scoreManager;

    int enemiesCount;

    private void Awake()
    {
        var canvas = Instantiate(_scoreCanvasPrefab);
        _scoreUI = canvas.GetComponent<ScoreUI>();
        _scoreManager = GetComponent<ScoreManager>();
        _enemyHandler = GetComponent<EnemiesHandler>();
    }

    private void Start()
    {

        FindAllEnemies();
        enemiesCount = enemiesToKill.Count;
        if (_scoreUI == null)
        {
            _scoreUI = FindFirstObjectByType<ScoreUI>();
        }
        _scoreUI.UpdateEnemiesToKillText(enemiesCount.ToString());
        _portal = GetComponentInChildren<SceneTransitionTrigger>();
        _portal.gameObject.SetActive(false);
    }

    private void DetectKill(EnemyHealth enemy, int collisionsCount)
    {
        enemy.OnDeath -= DetectKill;
        enemiesToKill.Remove(enemy);
        CheckIfCompleat();
        _scoreManager.HandleHit(collisionsCount);
        _scoreUI.UpdateEnemiesToKillText(enemiesToKill.Count.ToString());
    }

    private void CheckIfCompleat()
    {
        if (enemiesToKill.Count <= 0)
        {
            Debug.Log("LevelCompleat");
            CompleatLevel();
            OnLevelCompleat?.Invoke();
        }
    }

    private void FindAllEnemies()
    {
        foreach(EnemyHealth health in FindObjectsByType<EnemyHealth>(0))
        {
            enemiesToKill.Add(health);
            health.OnDeath += DetectKill;
        }
    }

    private void CompleatLevel()
    {
        _portal.gameObject.SetActive(true);
        Vector3 portalPosition = PlayerManager.Instance.GetPlayerTransform.position
                        + PlayerManager.Instance.GetPlayerTransform.forward * _distance;
        _portal.transform.position = portalPosition;
    }
}
