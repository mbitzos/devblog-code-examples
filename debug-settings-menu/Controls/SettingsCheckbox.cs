using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control to sync boolean properties with a checkbox 
/// </summary>
public class SettingsCheckbox : SettingsControl {

  [SerializeField]
  Toggle toggle;

  protected override void Start() {
    base.Start();

    // sync to toggle
    toggle.onValueChanged.AddListener(val => {
      Sync(val);
    });
  }

  protected override void Sync(object val) {
    base.Sync(val);
    toggle.isOn = val.GetType() == typeof(string) ? bool.Parse((string) val) : ((bool) val);
  }

  protected override string GetValue() {
    return toggle.isOn.ToString();
  }
}