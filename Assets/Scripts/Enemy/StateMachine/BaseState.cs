using UnityEngine;

public class BaseState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    protected BaseState(Enemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;       
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
