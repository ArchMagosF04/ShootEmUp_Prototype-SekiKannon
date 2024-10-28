using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;
    public IState CurrentState => currentState;

    public void Initialize(IState state)
    {
        currentState = state;
        currentState.OnEnter();
    }

    public void ChangeState(IState nextState) //Gets called to switch between states.
    {
        currentState.OnExit();
        currentState = nextState;
        currentState.OnEnter();

    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
    }
}
