/// <summary>
/// Models an input that the InputProfile registers
/// </summary>
public abstract class Input {

  public bool enabled;
  // ref to player
  protected PlayerController player;

  /// <summary>
  /// Creates a input
  /// </summary>
  /// <param name="player">Player reference</param>
  public Input(PlayerController player) {
    this.player = player;
  }

  /// <summary>
  /// Update loop
  /// </summary>
  public abstract void Update();

  /// <summary>
  /// What happens when the input is deactivated/stopped
  /// </summary>
  public virtual void Stop() { }

  /// <summary>
  /// What happens when the input is activated
  /// </summary>
  public virtual void Start() {
    this.enabled = true;
  }
}