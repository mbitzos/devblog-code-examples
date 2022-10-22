using UnityEngine;

namespace MBExample {
  /// <summary>
  /// Default state to control the player
  /// </summary>
  public class DefaultState : BasePlayerState {
    public override void OnEnter() { }

    public override void OnExit() { }

    public override void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        // No compile error because generics are awesome :) 
        this.stateObject.DoSomethingSpecificToPlayer();
      }
    }
  }
}