using UnityEngine;

public class EnemyDeathState: BaseState
{
    public EnemyDeathState(Enemy enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        // Отключаем движение
        enemy.Rb.freezeRotation = false;
        enemy.Rb.isKinematic = false;
        enemy.Rb.useGravity = true;
        
        Object.Destroy(enemy.gameObject, 7f);
    }

    public override void Update()
    {
        // Ничего не делаем — враг мёртв
    }
}
