using System;
using System.Collections.Generic;

/// <summary>
/// Models the manager for managing player input controllers
/// </summary>
public class InputManager {

  Dictionary<Type, InputProfile> inputControllers = new Dictionary<Type, InputProfile>();

  PlayerController player;
  public InputProfile currentInputController;

  public InputManager(PlayerController player) {
    this.player = player;

    /** register controllers, for example: **/
    // registerController(new MainInputController(player));
    // registerController(new OtherInputController(player));

    /** set default, for example: **/
    // this.ChangeController<MainInputController>();
  }

  void registerController(InputProfile controller) {
    inputControllers.Add(controller.GetType(), controller);
  }
  /// <summary>
  /// Update current inputcontroller
  /// </summary>
  public void Update() {
    if (currentInputController != null)
      currentInputController.Update();
  }

  /// <summary>
  /// Changes the input controller
  /// </summary>
  public void ChangeController<T>() where T : InputProfile {
    ChangeController(GetController<T>());
  }

  public void ChangeController(InputProfile controller) {
    if (currentInputController != null)
      currentInputController.OnDisable();
    currentInputController = controller;
    if (currentInputController != null)
      currentInputController.OnEnabled();
  }

  /// <summary>
  /// Turn off all controllers
  /// </summary>
  public void DisableInput() {
    ChangeController(null);
  }

  /// <summary>
  /// Returns a controller by name
  /// </summary>
  /// <param name="controller">The name of the controller</param>
  /// <typeparam name="T">The type of the controller</typeparam>
  /// <returns></returns>
  public T GetController<T>() where T : InputProfile {
    return (T) inputControllers[typeof(T)];
  }
}