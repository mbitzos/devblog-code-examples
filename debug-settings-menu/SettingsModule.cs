using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Models a collection of settings controls
/// </summary>
public class SettingsModule : MonoBehaviour {
  SettingsControl[] Controls;

  void Awake() {
    Controls = GetComponentsInChildren<SettingsControl>();
  }

  /// <summary>
  /// Loads the data
  /// </summary>
  /// <param name="data"></param>
  public void Load(Dictionary<string, string> data) {
    if (Controls != null)
      foreach (var control in Controls)
        control.Load(data);
  }

  /// <summary>
  /// Saves the data and returns a dictionary of keyvalues
  /// </summary>
  /// <returns></returns>
  public Dictionary<string, string> Save() {
    Dictionary<string, string> data = new Dictionary<string, string>();
    if (Controls != null)
      foreach (var control in Controls) {
        control.Save(data);
      }
    return data;
  }
}