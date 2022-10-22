using System.Collections.Generic;
using UnityEngine;

namespace GenericsExample {
  /// <summary>
  /// Models a generic-fsm for a player
  /// </summary>
  public class PlayerFSM : FiniteStateMachine<PlayerController> {
    public PlayerFSM(PlayerController player) : base(player) {
      var defaultState = new DefaultState(this, player);
      var states = new List<State<PlayerController>>() {
        defaultState,
        new DashState(this, player),
        new AttackState(this, player),
        new DeathState(this, player),
      };
      setStates(states, defaultState);
    }
  }

  public abstract class BasePlayerState : State<PlayerController> {
    protected BasePlayerState(PlayerFSM fsm, PlayerController player) : base(fsm, player) { }
  }
  /// <summary>
  /// Default state to control the player
  /// </summary>
  public class DefaultState : BasePlayerState {
    public DefaultState(PlayerFSM fsm, PlayerController player) : base(fsm, player) { }

    public override void FixedUpdate() { }

    public override void OnEnter() { }

    public override void OnExit() { }

    public override void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        // No compile error because generics are awesome :) 
        this.stateObject.DoSomethingSpecificToPlayer();
      }
    }
  }

  /// <summary>
  /// Models when the player is performing an attack 
  /// </summary>
  public class AttackState : BasePlayerState {
    public AttackState(PlayerFSM fsm, PlayerController player) : base(fsm, player) { }

    public override void OnEnter() {
      // Start attack animation
    }

    public override void OnExit() {
      // End attack animation
    }

    public override void Update() { }

    public override void FixedUpdate() {
      // perform attack physics movement
    }
  }

  /// <summary>
  /// Models the state when the player dashes
  /// </summary>
  public class DashState : BasePlayerState {
    public DashState(PlayerFSM fsm, PlayerController player) : base(fsm, player) { }

    public override void OnEnter() {
      // start dash animation
      // start i-frames
    }

    public override void OnExit() {
      // stop i-frames
    }

    public override void FixedUpdate() {
      // perform dash physics
    }
    public override void Update() { }
  }

  /// <summary>
  /// Models the state when the player is dead 
  /// </summary>
  public class DeathState : BasePlayerState {
    public DeathState(PlayerFSM fsm, PlayerController player) : base(fsm, player) { }

    public override void OnEnter() {
      // death animation
      // game over
      // stop inputs
    }

    public override void OnExit() { }

    public override void Update() { }

    public override void FixedUpdate() { }
  }
}