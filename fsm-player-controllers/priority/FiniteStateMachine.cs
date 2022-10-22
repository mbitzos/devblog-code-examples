using System;
using System.Collections.Generic;

namespace PriorityExample {
  /// <summary>
  /// Models the finite state machine that has the concept of state priorites
  /// </summary>
  public class FiniteStateMachine {

    Dictionary<Type, State> states = new Dictionary<Type, State>();

    public State currentState {
      get;
      private set;
    }
    State defaultState;

    PlayerController player;

    /// <summary>
    /// Constructs a new finite state machine
    /// </summary>
    public FiniteStateMachine(PlayerController player) {
      this.player = player;
    }

    /// <summary>
    /// Initializes the FSM with the states 
    /// </summary>
    protected void setStates(List<State> states, State defaultState) {
      this.states.Clear();
      foreach (var state in states) {
        this.states.Add(state.GetType(), state);
      }
      this.defaultState = defaultState;
    }

    /// <summary>
    /// Starts the FSM
    /// </summary>
    public void Start() {
      this.ChangeState(defaultState);
    }

    /// <summary>
    /// Triggers an update
    /// </summary>
    public void Update() {
      if (currentState != null)
        currentState.Update();
    }

    /// <summary>
    /// triggers a fixed Update
    /// </summary>
    public void FixedUpdate() {
      if (currentState != null)
        currentState.FixedUpdate();
    }

    /// <summary>
    /// Change to new state
    /// </summary>
    /// <typeparam name="S">The state to change to</typeparam>
    /// <returns>if state change was successful</returns>
    public bool ChangeState<S>() where S : State {
      return ChangeState(states[typeof(S)]);
    }

    /// <summary>
    /// Change to new state
    /// </summary>
    /// <param name="state"></param>
    /// <returns>if state change was successful</returns>
    public bool ChangeState(State state) {
      if (currentState != null) {

        // Here we could introduce the concept of configurable FSM priority behaviour.
        // incase some FSMs would like a "meets-it-beats" style or strictly greater priority change
        if (currentState.priority > state.priority)
          return false;
        currentState.OnExit();
      }
      currentState = state;
      currentState.OnEnter();
      return true;
    }

    /// <summary>
    /// Changes to current state
    /// </summary>
    /// <returns>if state change was successful</returns>
    public bool ChangeToDefault() {
      return ChangeState(defaultState);
    }
  }
}