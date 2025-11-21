using UnityEngine;

public class EnemySwichCollider : MonoBehaviour
{
    [SerializeField] private Collider _aliveCollider;
    [SerializeField] private Collider _deadCollider;
    [SerializeField] private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        _aliveCollider.enabled = true;
        _deadCollider.enabled = false;
    }

    private void OnEnable()
    {
        enemyHealth.OnDeath += SwitchCollider;
    }

    private void OnDisable()
    {
        enemyHealth.OnDeath -= SwitchCollider;
    }

    private void SwitchCollider(EnemyHealth enemy)
    {
        Debug.Log("SwitchCollider");
        _aliveCollider.enabled = false;
        _deadCollider.enabled = true;
    }
}
