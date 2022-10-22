namespace MBExample {

  /// <summary>
  /// Models a monobehaviour FSM
  /// NOTE: with this implementation you'll notice that we do not have any implementation for FSM since all is done by our parent
  /// </summary>
  public class PlayerFSM : FiniteStateMachine<MBPlayerController> { }

  public abstract class BasePlayerState : State<MBPlayerController> { }
}