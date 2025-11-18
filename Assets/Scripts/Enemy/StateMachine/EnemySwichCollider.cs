using UnityEngine;

public class EnemySwichCollider : MonoBehaviour
{
    [SerializeField] private Collider _aliveCollider;
    [SerializeField] private Collider _deadCollider;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.OnDeath += SwitchCollider;
        _aliveCollider.enabled = true;
        _deadCollider.enabled = false;
    }

    private void OnDisable()
    {
        enemyHealth.OnDeath -= SwitchCollider;
    }

    private void SwitchCollider()
    {
        _aliveCollider.enabled = false;
        _deadCollider.enabled = true;
    }
}
