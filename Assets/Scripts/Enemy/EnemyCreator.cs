using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private PhysicsObjectsRegistry _physicsObjectsRegistry;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _timeCreate = 5;


    private void Start()
    {
        if (_physicsObjectsRegistry == null)
        {
            _physicsObjectsRegistry = FindAnyObjectByType<PhysicsObjectsRegistry>();
        }
        InvokeRepeating("EnemyCreate", 10, _timeCreate);
    }


    public void EnemyCreate()
    {
        GameObject newEnemy = Instantiate(_enemyPrefab, _spawn.position, _spawn.rotation);
        if (newEnemy.TryGetComponent(out Rigidbody rb))
        {
            _physicsObjectsRegistry.RegisterNewRigitbody(rb);
        }
    }
}
