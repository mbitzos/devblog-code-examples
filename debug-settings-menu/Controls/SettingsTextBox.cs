using System;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Models a textbox that allows for string settings
/// </summary>
public class SettingsTextBox : SettingsControl {
  public TMP_InputField inputField;

  public int DecimalPlaces = 2;

  protected override void Start() {
    base.Start();

    // sync to toggle
    inputField.onValueChanged.AddListener(val => {
      Sync(val);
    });
  }

  protected override string GetValue() {
    return inputField.text;
  }
}