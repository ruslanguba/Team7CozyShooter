using UnityEngine;
using System.Collections.Generic;

public class EnemiesHandler : MonoBehaviour
{
    public List<EnemyHealth> AllEnemies => enemies;

    private List<EnemyHealth> enemies;

    //private void Awake()
    //{
    //    FindAllEnemies();
    //}

    //private void FindAllEnemies()
    //{
    //    foreach (EnemyHealth health in FindObjectsByType<EnemyHealth>(0))
    //    {
    //        enemies.Add(health);
    //    }
    //}
}
