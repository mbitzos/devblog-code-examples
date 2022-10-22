using UnityEngine;

namespace MBExample {

  /// <summary>
  /// Models a monobehaviour state in an generic-monobehaviour FSM
  /// </summary>
  /// <typeparam name="SO">[SO=State Object] This is the object that our FSM is controlling state for </typeparam>
  public abstract class State<SO> : MonoBehaviour where SO : MonoBehaviour {
    protected FiniteStateMachine<SO> fsm;
    protected SO stateObject;

    void Start() {
      stateObject = this.GetComponent<SO>();
      fsm = this.GetComponent<FiniteStateMachine<SO>>();
    }

    /// <summary>
    /// What to do on entering the state (not to be confused with the OnFirstUpdate beat)
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// What to do on exiting the state
    /// </summary>
    public abstract void OnExit();

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

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