using PriorityExample;
using UnityEngine;

/// <summary>
/// A very basic example to showcase the usage of the FSM
/// NOTE: this doesn't work, use this as just a written example
/// </summary>
public class PlayerController : MonoBehaviour {

  private PlayerFSM fsm;
  int hp = 10;

  void Start() {
    fsm = new PlayerFSM(this);
    fsm.Start();
  }

  void Update() {
    fsm.Update();
  }

  void GetAttacked() {
    if (--hp <= 0) {
      fsm.ChangeState<DeathState>();
    } else {
      fsm.ChangeState<AttackState>();
    }
  }

  public void DoSomethingSpecificToPlayer() {
    Debug.Log("Player action!");
  }

  void FixedUpdate() {
    fsm.FixedUpdate();
  }
}