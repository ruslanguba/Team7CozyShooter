
using UnityEngine;

public class EnemyPatrolState: BaseState
{
    private int index;
    private Vector3 _target;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        index = 0;
    }

    public override void Update()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.PlayerTransform.position) < enemy.ChaseRange)
        {
            stateMachine.ChangeState(enemy.ChaseState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        Vector3 direction;
        if (enemy.PatrolPoints.Length > 0 && enemy.PatrolPoints[index] != null)
        {
            _target = enemy.PatrolPoints[index].position;
            direction = (_target - enemy.transform.position).normalized;

            MoveTowards(direction);
        }
        else
        {
            enemy.RotationHandler.RoteteToPlayer(enemy.PlayerTransform.position);       
        }

        if (Vector3.Distance(enemy.transform.position, _target) < 0.5f)
        {
            index = (index + 1) % enemy.PatrolPoints.Length;
        }
    }

    private void MoveTowards(Vector3 direction)
    {
        Vector3 targetVelocity = direction * enemy.MoveSpeed;
        Vector3 velocityDiff = targetVelocity - enemy.Rb.linearVelocity;

        enemy.Rb.AddForce(velocityDiff * enemy.Acceleration, ForceMode.Acceleration);

        enemy.ActorAudio.TickFootsteps(true);
    }
}
