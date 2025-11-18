
using UnityEngine;

public class EnemyPatrolState: BaseState
{
    private int index;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        index = 0;
    }

    public override void Update()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Target.position) < enemy.ChaseRange)
        {
            stateMachine.ChangeState(enemy.ChaseState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        Vector3 target;
        Vector3 direction;
        if (enemy.PatrolPoints.Length == 0)
        {
            target = enemy.Target.position;
        }
        else
        {
            target = enemy.PatrolPoints[index].position;
        }
        direction = (target - enemy.transform.position).normalized;
        MoveTowards(direction);

        if (Vector3.Distance(enemy.transform.position, target) < 0.5f)
        {
            index = (index + 1) % enemy.PatrolPoints.Length;
        }
    }

    private void MoveTowards(Vector3 direction)
    {
        Vector3 targetVelocity = direction * enemy.MoveSpeed;
        Vector3 velocityDiff = targetVelocity - enemy.Rb.linearVelocity;

        enemy.Rb.AddForce(velocityDiff * enemy.Acceleration, ForceMode.Acceleration);
    }
}
