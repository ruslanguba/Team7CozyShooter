using UnityEngine;

public class EnemyDeathState: BaseState
{
    public EnemyDeathState(Enemy enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        // Отключаем движение
        //enemy.Rb.linearVelocity = Vector3.zero;
        enemy.Rb.isKinematic = false;
        enemy.Rb.useGravity = true;
        // включаем анимацию, если есть
        //enemy.Animator?.SetTrigger("Die");

        // отключаем коллайдеры если надо
        //foreach (var col in enemy.Colliders)
        //    col.enabled = false;

        // Можно запланировать удаление
        
        Object.Destroy(enemy.gameObject, 7f);
    }

    public override void Update()
    {
        // Ничего не делаем — враг мёртв
    }
}
