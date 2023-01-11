using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control to sync a number (int or decimal) to a slider component
/// </summary>
public class SettingsSlider : SettingsControl {
  public Slider slider;
  public Text textvalue;

  public int DecimalPlaces = 2;

  protected override void Start() {
    base.Start();

    // sync to toggle
    slider.onValueChanged.AddListener(val => {
      Sync(val);
    });
  }

  protected override void Sync(object val) {
    base.Sync(val);

    // slider precision
    if (slider.wholeNumbers) {
      int vall = Convert.ToInt32(val);
      slider.value = vall;
      textvalue.text = vall.ToString();
    } else {
      float vall = val.GetType() == typeof(string) ? float.Parse((string) val) : ((float) val);
      slider.value = vall;
      textvalue.text = Math.Round(vall, DecimalPlaces).ToString();
    }
  }

  protected override string GetValue() {
    return slider.value.ToString();
  }
}