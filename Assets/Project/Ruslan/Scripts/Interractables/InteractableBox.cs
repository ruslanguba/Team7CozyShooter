using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : InteractableBase
{
    [SerializeField] Animator _animator;
    [SerializeField] private List<SimpleRunEnemy> _enemies;
    [SerializeField] private List<Transform> _nextPointsTransform;

    [SerializeField] private bool _isRandomNextPoin;
    [SerializeField] private float _hideDuration;
    [SerializeField] private float _openDuration;
    [SerializeField] private float _closeDuration;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        foreach (var enemy in _enemies)
        {
            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(false);
        }
        CloseDoor();
    }
    
    public override void OnInteract()
    {
        StartEnemyRun();
    }

    public void ReciveEnemy(SimpleRunEnemy enemy)
    {
        StartCoroutine(MoveAndShrink(enemy));
    }


    public void CloseDoor()
    {
        _animator.SetBool("isOpen", false);
        if(_enemies.Count > 0)
            _animator.SetBool("isEnemy", true);
    }

    private void StartEnemyRun()
    {
        _animator.SetTrigger("isOpen");
        for (int i = _enemies.Count -1; i >= 0; i--)
        {
            int rndIndex = Random.Range(0, _nextPointsTransform.Count);
            _enemies[i].transform.position = transform.position + transform.forward;
            _enemies[i].SetRunTarget(_nextPointsTransform[rndIndex]);
            _enemies[i].gameObject.SetActive(true);
            _enemies[i].transform.localScale = Vector3.one;
            _enemies.Remove(_enemies[i]);
        }
    }

    public IEnumerator MoveAndShrink(SimpleRunEnemy enemy)
    {
        Vector3 startPos = enemy.transform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 startScale = enemy.transform.localScale;
        Vector3 endScale = Vector3.one * 0.01f;
        _animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(_openDuration);
        float t = 0f;

        while (t < _hideDuration)
        {
            t += Time.deltaTime;
            float normalized = t / _hideDuration;

            // Движение
            enemy.transform.position = Vector3.Lerp(startPos, endPos, normalized);

            // Уменьшение масштаба
            enemy.transform.localScale = Vector3.Lerp(startScale, endScale, normalized);

            yield return null;
        }
        yield return new WaitForSeconds(_closeDuration);

        CloseDoor();
        // Финальная фиксация
        enemy.transform.position = endPos;
        enemy.transform.localScale = endScale;
        enemy.gameObject.SetActive(false);
        if(enemy != null)
        {
            _enemies.Add(enemy);
            enemy.OnDestroyed += RemoveEnemy;
        }
    }

    private void RemoveEnemy(SimpleRunEnemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
