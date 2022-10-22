using UnityEngine;

namespace GenericsExample {

  /// <summary>
  /// Models a state in an generic-powered FSM
  /// </summary>
  /// <typeparam name="SO">[SO=State Object] This is the object that our FSM is controlling state for </typeparam>
  public abstract class State<SO> where SO : MonoBehaviour {
    protected FiniteStateMachine<SO> fsm;
    protected SO stateObject;

    public State(FiniteStateMachine<SO> fsm, SO stateObject) {
      this.stateObject = stateObject;
      this.fsm = fsm;
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
    public void Goto<S>() where S : State<SO> {
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