using System;
using System.Collections.Generic;

///  <summary>
/// Models an abstract input controller
/// </summary>
public abstract class InputProfile {
  protected Dictionary<Type, Input> inputMapping = new Dictionary<Type, Input>();

  public InputProfile(PlayerController player) {
    foreach (Input input in GetInputs(player)) {
      inputMapping[input.GetType()] = input;
    }
  }

  /// <summary>
  /// Defines which inputs are for this controller
  /// </summary>
  /// <returns></returns>
  protected abstract List<Input> GetInputs(PlayerController player);

  /// <summary>
  /// Disables an input
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public void DisableInput<T>() where T : Input {
    setInputState<T>(false);
  }

  /// <summary>
  /// Enables a specific input
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public void EnableInput<T>() where T : Input {
    setInputState<T>(true);
  }

  private void setInputState<T>(bool value) {
    var input = typeof(T);
    if (inputMapping.ContainsKey(input)) {
      inputMapping[input].enabled = value;
    }
  }

  /// <summary>
  /// What happens when this controller loses control
  /// </summary>
  public virtual void OnEnabled() {
    foreach (Input input in inputMapping.Values) {
      input.Start();
    }
  }

  /// <summary>
  /// What happens when this controller gains control
  /// </summary>
  public virtual void OnDisable() {
    foreach (Input input in inputMapping.Values) {
      input.Stop();
    }
  }

  /// <summary>
  /// Updates all inputs
  /// </summary>
  public virtual void Update() {
    foreach (Input input in inputMapping.Values) {
      if (input.enabled) {
        input.Update();
      }
    }

  }
}