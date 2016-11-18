using System;

public class Transition {

    private string name;
    public string Name { get { return name; } }
    private Func<bool> listener;

    private State from;
    public State From { get { return from; } }
    private State to;
    public State To { get { return to; } }

    public Transition(State fromState, State toState, Func<bool> listener)
    {
        this.from = fromState;
        this.to = toState;
        this.listener = listener;
        this.name = fromState.Name + " to " + toState.Name;
    }

    public Transition(State fromState, State toState, Func<bool> listener, string name)
    {
        this.from = fromState;
        this.to = toState;
        this.listener = listener;
        this.name = name;
    }

	public bool Listen()
    {
        return listener();
    }

}
