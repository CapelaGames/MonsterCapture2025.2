using UnityEngine;

public class StateMachine
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateSM()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}
