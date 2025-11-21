using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HidenEnemyManager : MonoBehaviour
{
    [SerializeField] private SimpleRunEnemy _runEnemyPrefab;
    [SerializeField] private int _hiddenEnemiesCount;
    [SerializeField] private LevelProgressSystem _levelProgressSystem;

    [SerializeField] private bool _isRandom;

    List<SimpleRunEnemy> simpleRunEnemies = new List<SimpleRunEnemy>();
    List<Transform> _boxesTransforms = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < _hiddenEnemiesCount; i++)
        {
            Instantiate(simpleRunEnemies[i]);
            simpleRunEnemies.Add(simpleRunEnemies[i]);
            FindAllBoxes();
        }
        FindAllBoxes();
        RandomisePosiitions();
    }

    private void FindAllBoxes()
    {
        foreach (InteractableBox t in FindObjectsByType<InteractableBox>(0))
            _boxesTransforms.Add(t.transform);
    }

    private void RandomisePosiitions()
    {
        int random = Random.Range(0, _boxesTransforms.Count);
    }
}
