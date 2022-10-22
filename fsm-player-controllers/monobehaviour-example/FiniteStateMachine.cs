using System;
using System.Collections.Generic;
using UnityEngine;

namespace MBExample {
  /// <summary>
  /// Models the monobehaviour finite state machine using generics
  /// </summary>
  /// <typeparam name="SO">[SO=State Object] This is the object that our FSM is controlling state for </typeparam>
  public class FiniteStateMachine<SO> : MonoBehaviour where SO : MonoBehaviour {

    Dictionary<Type, State<SO>> states = new Dictionary<Type, State<SO>>();
    public State<SO> defaultState;

    public State<SO> currentState {
      get;
      private set;
    }

    // This is the object that our FSM is controlling state for
    SO stateObject;

    /// <summary>
    /// NOTE: you could have this controlled by a function instead of on Start hook
    /// </summary>
    void Start() {

      // Get states
      this.states.Clear();
      foreach (var state in this.GetComponents<State<SO>>()) {
        state.enabled = false;
        this.states.Add(state.GetType(), state);
      }
      this.ChangeState(defaultState);
    }

    /// <summary>
    /// Change to new state
    /// </summary>
    /// <typeparam name="S">The state to change to</typeparam>
    public void ChangeState<S>() where S : State<SO> {
      ChangeState(states[typeof(S)]);
    }

    /// <summary>
    /// Change to new state, here we are using unity's enable state to turn off updates
    /// </summary>
    /// <typeparam name="S">The state to change to</typeparam>
    public void ChangeState(State<SO> state) {
      if (currentState != null) {
        currentState.OnExit();
        currentState.enabled = false;
      }
      currentState = state;
      currentState.enabled = true;
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