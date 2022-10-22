namespace PayloadExample {

  /// <summary>
  /// Models a state in an FSM that can send & receive payloads
  /// </summary>
  public abstract class State {
    protected FiniteStateMachine fsm;
    protected PlayerController player;

    public State(FiniteStateMachine fsm, PlayerController player) {
      this.player = player;
      this.fsm = fsm;
    }

    /// <summary>
    /// What to do on entering the state
    /// </summary>
    public abstract void OnEnter(object payload = null);

    /// <summary>
    /// What to do on exiting the state
    /// </summary>
    public abstract void OnExit();

    public abstract void Update();
    public abstract void FixedUpdate();

    /// <summary>
    /// Goes to a new state (Wrapper for ChangeState)
    /// </summary>
    public void Goto<S>(object payload = null) where S : State {
      fsm.ChangeState<S>(payload);
    }

    /// <summary>
    /// Changes to default
    /// </summary>
    public void GoToDefault(object payload = null) {
      fsm.ChangeToDefault(payload);
    }

    /// <summary>
    /// Attempts to get the current payload
    /// <param name="defaultt">The default value to return</param>
    /// <typeparam name="P">The type to cast to</typeparam>
    protected P CastPayload<P>(object payload, P defaultt = default(P)) {
      try {
        return (P) payload;
      } catch {
        return defaultt;
      }
    }
  }
}