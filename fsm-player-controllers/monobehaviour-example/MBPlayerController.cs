using UnityEngine;

namespace MBExample {
  /// <summary>
  /// A very basic example to showcase the usage of the FSM
  /// </summary>
  public class MBPlayerController : MonoBehaviour {

    int hp = 10;

    PlayerFSM fsm;

    void Start() {
      fsm = GetComponent<PlayerFSM>();
    }

    void GetAttacked() {
      if (--hp <= 0) {
        fsm.ChangeState<DeathState>();
      }
    }

    public void DoSomethingSpecificToPlayer() {
      Debug.Log("Player action!");
      GetAttacked();
    }
  }
}