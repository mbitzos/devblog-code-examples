namespace MBExample {

  /// <summary>
  /// Models the state when the player is dead 
  /// </summary>
  public class DeathState : BasePlayerState {
    public override void OnEnter() {
      // death animation
      // game over
      // stop inputs
    }

    public override void OnExit() { }
  }
}