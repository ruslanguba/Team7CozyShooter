using UnityEngine;

public class EnemyStateMachine
{
    public BaseState CurrentState { get; private set; }

    public void Initialize(BaseState startState)
    {
        CurrentState = startState;
        startState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}
