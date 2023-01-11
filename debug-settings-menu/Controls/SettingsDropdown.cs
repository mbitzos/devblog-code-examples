using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Setting controls for Enums, values are matched to the order not the value
/// </summary>
public class SettingsDropdown : SettingsControl {

  [SerializeField]
  TMP_Dropdown dropdown;

  protected override void Start() {
    base.Start();

    setOptions();

    // sync to toggle
    dropdown.onValueChanged.AddListener(val => {
      Sync(val);
    });
  }

  protected virtual void setOptions() {
    Type enumType = initial.GetType();
    dropdown.options = Enum.GetNames(enumType).Select(name => new TMP_Dropdown.OptionData(name)).ToList();
  }

  protected override void Sync(object val) {
    var enumValue = Enum.Parse(initial.GetType(), val.ToString());
    base.Sync(enumValue);
    dropdown.value = (int) enumValue;
  }

  protected override string GetValue() {
    return dropdown.value.ToString();
  }
}