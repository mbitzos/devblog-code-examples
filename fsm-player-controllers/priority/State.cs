namespace PriorityExample {

  /// <summary>
  /// Models a state in an FSM that has priority
  /// </summary>
  public abstract class State {
    protected FiniteStateMachine fsm;
    protected PlayerController player;
    public int priority {
      get;
      private set;
    }

    public State(FiniteStateMachine fsm, PlayerController player, int priority = -1) {
      this.player = player;
      this.fsm = fsm;
      this.priority = priority;
    }

    /// <summary>
    /// What to do on entering the state
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// What to do on exiting the state
    /// </summary>
    public abstract void OnExit();

    public abstract void Update();
    public abstract void FixedUpdate();

    /// <summary>
    /// Goes to a new state (Wrapper for ChangeState)
    /// </summary>
    public void Goto<S>() where S : State {
      fsm.ChangeState<S>();
    }

    /// <summary>
    /// Changes to default
    /// </summary>
    public void GoToDefault() {
      fsm.ChangeToDefault();
    }
  }
}