using System.Collections.Generic;
using UnityEngine;

namespace PriorityExample {
  /// <summary>
  /// Models a basic fsm for a player
  /// </summary>
  public class PlayerFSM : FiniteStateMachine {
    public PlayerFSM(PlayerController player) : base(player) {
      var defaultState = new DefaultState(this, player);
      var states = new List<State>() {
        defaultState,
        new DashState(this, player),
        new AttackState(this, player, 1),
        new DeathState(this, player, 2),
      };
      setStates(states, defaultState);
    }
  }

  /// <summary>
  /// Default state to control the player
  /// </summary>
  public class DefaultState : State {
    public DefaultState(FiniteStateMachine fsm, PlayerController player, int priority = -1) : base(fsm, player, priority) { }

    public override void FixedUpdate() { }

    public override void OnEnter() { }

    public override void OnExit() { }

    public override void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        Goto<DashState>();
      }
      // handle basic player movement physics + animation
    }
  }

  /// <summary>
  /// Models when the player is performing an attack 
  /// </summary>
  public class AttackState : State {
    public AttackState(FiniteStateMachine fsm, PlayerController player, int priority = -1) : base(fsm, player, priority) { }

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
  public class DashState : State {
    public DashState(FiniteStateMachine fsm, PlayerController player, int priority = -1) : base(fsm, player, priority) { }

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
  public class DeathState : State {
    public DeathState(FiniteStateMachine fsm, PlayerController player, int priority = -1) : base(fsm, player, priority) { }

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