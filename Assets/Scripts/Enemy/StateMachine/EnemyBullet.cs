using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerDamageReciver playerDamageReciver))
        {
            playerDamageReciver.TakeDamage();
            Destroy(gameObject);
        }
    }
}
