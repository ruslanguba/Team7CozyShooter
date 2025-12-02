using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 6f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerDamageReciver playerDamageReciver))
        {
            Vector3 knockDir = (other.transform.position - transform.position).normalized;
            playerDamageReciver.TakeDamage(knockDir, knockbackForce);
            Destroy(gameObject);
        }
    }
}
