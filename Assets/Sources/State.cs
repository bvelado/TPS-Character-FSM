using System;
using System.Collections.Generic;

public class State {

    private string name;
    public string Name { get { return name; } }

    private Action enter;
    private Action execute;
    private Action exit;

    private Transition[] transitions;

    public State(string name, Action enter = null, Action execute = null, Action exit = null)
    {
        this.name = name;
        transitions = new Transition[0];
        
        if (enter != null)
            this.enter = enter;

        if (execute != null)
            this.execute = execute;

        if (exit != null)
            this.exit = exit;
    }

    public void Enter() {
        if (enter != null)
            enter();
    }

    public void Execute()
    {
        if (execute != null)
            execute();
    }

    public void Exit()
    {
        if (exit != null)
            exit();
    }

    public Transition CheckTransitions()
    {
        foreach(var transition in transitions)
        {
            if (transition.Listen())
                return transition;
        }
        return null;
    }

    public State AddTransition(Transition transition)
    {
        var newTransitions = new List<Transition>();
        newTransitions.AddRange(transitions);
        newTransitions.Add(transition);
        transitions = newTransitions.ToArray();

        return this;
    }

    public State AddTransition(State toState, Func<bool> listener)
    {
        Transition newTransition = new Transition(this, toState, listener);

        var newTransitions = new List<Transition>();
        newTransitions.AddRange(transitions);
        newTransitions.Add(newTransition);
        transitions = newTransitions.ToArray();

        return this;
    }
}
