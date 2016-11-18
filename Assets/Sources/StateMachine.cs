using UnityEngine;
using System.Collections;

public class StateMachine
{
    private State[] states;
    private State currentState;

    private Transition t;

    public StateMachine(State[] states, int initialStateIndex = 0)
    {
        if (states.Length > 0)
            this.states = states;
        else
            Debug.LogWarning("No state provided for " + this + ".");


        if (initialStateIndex == 0)
        {
            Debug.LogWarning("No initial state index provided for " + this + ". Initializing with " + states[0].Name + ".");
        }

        currentState = states[initialStateIndex];

        Enter();
    }

    void Enter()
    {
        currentState.Enter();
    }

    public void Execute()
    {
        t = currentState.CheckTransitions();

        if (t != null)
            ChangeState(t.To);
        else
        currentState.Execute();
    }

    void Exit()
    {
        currentState.Exit();
    }

    void ChangeState(State to)
    {
        Debug.Log("Changing state to " + to.Name);
        currentState.Exit();
        currentState = to;
        Enter();
    }
}
