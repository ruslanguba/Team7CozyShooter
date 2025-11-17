using UnityEngine;

public class EnemyAttackState : BaseState
{
    private float attackTimer;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        //attackTimer = 0f; // сразу можно атаковать
        enemy.Rb.linearVelocity = Vector3.zero;
    }

    public override void Update()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.Target.position);

        // Если игрок ушёл из зоны атаки — возвращаемся к преследованию
        if (dist > enemy.AttackRange)
        {
            stateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        // Таймер атаки
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            ThrowProjectile();
            attackTimer = enemy.AttackCooldown;
        }
    }

    private void ThrowProjectile()
    {
        if (enemy.AttackPrefab != null && enemy.AttackSpawn != null)
        {
            GameObject projectile = GameObject.Instantiate(enemy.AttackPrefab, enemy.AttackSpawn.position, Quaternion.identity);
            if (projectile.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Vector3 dir = (enemy.Target.position - enemy.AttackSpawn.position).normalized;
                rb.AddForce(dir * enemy.ThrowForce, ForceMode.Impulse);
            }
        }
    }
}
