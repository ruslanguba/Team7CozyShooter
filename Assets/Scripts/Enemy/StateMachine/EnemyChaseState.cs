using UnityEngine;

public class EnemyChaseState: BaseState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Update()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.Target.position);

        if (dist < enemy.AttackRange)
        {
            stateMachine.ChangeState(enemy.AttackState);
            return;
        }

        if (dist > enemy.ChaseRange)
        {
            stateMachine.ChangeState(enemy.PatrolState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        Vector3 dir = (enemy.Target.position - enemy.transform.position).normalized;

        MoveTowards(dir);
    }

    private void MoveTowards(Vector3 direction)
    {
        Vector3 targetVelocity = direction * enemy.MoveSpeed;
        Vector3 velocityDiff = targetVelocity - enemy.Rb.linearVelocity;

        enemy.Rb.AddForce(velocityDiff * enemy.Acceleration, ForceMode.Acceleration);
    }
}
