using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressSystem : MonoBehaviour
{
    public event Action OnEnemeyKilled;
    public event Action OnLevelCompleat;
    [SerializeField] private List<Enemy> enemiesToKill;
    int enemiesCount;

    private void OnEnable()
    {
        foreach (Enemy e in enemiesToKill)
        {
            if (e.gameObject.TryGetComponent(out EnemyHealth health))
            {
                health.OnDeath += DetectKill;
            }
        }
        enemiesCount = enemiesToKill.Count;
    }

    private void DetectKill()
    {
        enemiesCount--;
        CheckIfCompleat();
        //if (gameObject.TryGetComponent(out Enemy enemy))
        //{   enemy.GetComponent<EnemyHealth>().OnDeath -= DetectKill;
        //    if (enemiesToKill.Contains(enemy))
        //    {
        //        enemiesToKill.Remove(enemy);
        //        CheckIfCompleat();
        //    }
        //}
    }

    private void CheckIfCompleat()
    {
        if (enemiesCount == 0)
        {
            Debug.Log("LevelCompleat");
            OnLevelCompleat?.Invoke();
        }
        //if(enemiesToKill.Count <= 0)
        //{
        //    Debug.Log("LevelCompleat");
        //}
    }
}
