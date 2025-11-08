using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _timeCreate = 5;


    private void Start()
    {
        InvokeRepeating("EnemyCreate", 10, _timeCreate);
    }


    public void EnemyCreate()
    {
        Instantiate(_enemyPrefab, _spawn.position, _spawn.rotation);
    }
}
