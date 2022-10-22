using System.Collections.Generic;
using UnityEngine;

namespace PayloadExample {
  /// <summary>
  /// Models a fsm that uses payload for a player
  /// </summary>
  public class PlayerFSM : FiniteStateMachine {
    public PlayerFSM(PlayerController player) : base(player) {
      var defaultState = new DefaultState(this, player);
      var states = new List<State>() {
        defaultState,
      };
      setStates(states, defaultState);
    }
  }

  /// <summary>
  /// Default state to control the player
  /// </summary>
  public class DefaultState : State {
    public DefaultState(FiniteStateMachine fsm, PlayerController player) : base(fsm, player) { }

    public override void OnEnter(object payload = null) {
      bool shouldDoOperation = this.CastPayload<bool>(payload, true);
      // do something with this
    }

    public override void OnExit() { }

    public override void FixedUpdate() { }

    public override void Update() { }
  }

}