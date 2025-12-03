using UnityEngine;

public class EnemyDeathState: BaseState
{

    public EnemyDeathState(Enemy enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) 
    { }

    public override void Enter()
    {
        enemy.RotationHandler.enabled = false;
        // Отключаем движение
        enemy.Rb.freezeRotation = false;
        enemy.Rb.isKinematic = false;
        enemy.Rb.useGravity = true;

        enemy.ActorAudio.PlayDeath();
        enemy.Animator.SetTrigger("dead");
        Object.Destroy(enemy.gameObject, 7f);
    }
}
