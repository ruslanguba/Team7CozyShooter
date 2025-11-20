using UnityEngine;

public class EnemyAttackState : BaseState
{
    private float attackTimer;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        enemy.RotationHandler.enabled = false;
        enemy.Rb.linearVelocity = Vector3.zero;
        Vector3 dir = enemy.PlayerTransform.position - enemy.transform.position;
        dir.y = 0;
        enemy.transform.rotation = Quaternion.LookRotation(dir);
    }

    public override void Update()
    {
        enemy.RotationHandler.RoteteToPlayer(enemy.PlayerTransform.position);
        float dist = Vector3.Distance(enemy.transform.position, enemy.PlayerTransform.position);

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
    private void RoteteToPlayer()
    {
        Vector3 dir = enemy.PlayerTransform.position - enemy.transform.position;
        dir.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRot, 8f * Time.deltaTime);
    }

    private void ThrowProjectile()
    {
        if (enemy.AttackPrefab != null && enemy.AttackSpawn != null)
        {
            GameObject projectile = Object.Instantiate(enemy.AttackPrefab, enemy.AttackSpawn.position, Quaternion.identity);
            Object.Destroy(projectile.gameObject, 5);
            if (projectile.TryGetComponent(out Rigidbody rb))
            {
                Vector3 dir = (enemy.PlayerTransform.position - enemy.AttackSpawn.position).normalized;
                rb.AddForce(dir * enemy.ThrowForce, ForceMode.Impulse);
                //enemy.ActorAudio.PlayAttack();
            }
        }
    }

    public override void Exit()
    {
        enemy.RotationHandler.enabled = true;
    }
}
