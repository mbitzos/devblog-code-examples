using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenericsExample {
  /// <summary>
  /// Models the finite state machine using generics
  /// </summary>
  /// <typeparam name="SO">[SO=State Object] This is the object that our FSM is controlling state for </typeparam>
  public class FiniteStateMachine<SO> where SO : MonoBehaviour {

    Dictionary<Type, State<SO>> states = new Dictionary<Type, State<SO>>();

    public State<SO> currentState {
      get;
      private set;
    }
    State<SO> defaultState;

    // This is the object that our FSM is controlling state for
    private SO stateObject;

    /// <summary>
    /// Constructs a new finite state machine
    /// </summary>
    public FiniteStateMachine(SO stateObject) {
      this.stateObject = stateObject;
    }

    /// <summary>
    /// Initializes the FSM with the states 
    /// </summary>
    protected void setStates(List<State<SO>> states, State<SO> defaultState) {
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
    public void ChangeState<S>() where S : State<SO> {
      ChangeState(states[typeof(S)]);
    }

    /// <summary>
    /// Change to new state 
    /// </summary>
    /// <typeparam name="S">The state to change to</typeparam>
    public void ChangeState(State<SO> state) {
      if (currentState != null) {
        currentState.OnExit();
      }
      currentState = state;
      currentState.OnEnter();
    }

    /// <summary>
    /// Changes to current state
    /// </summary>
    public void ChangeToDefault() {
      ChangeState(defaultState);
    }
  }
}