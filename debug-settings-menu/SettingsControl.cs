using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Models a settings control
/// </summary>
public abstract class SettingsControl : MonoBehaviour {

  // The script we are targetting
  public MonoBehaviour script;

  // the parameter on the script we are targetting
  private string parameter;

  // if this parameter requires a soft game reload
  public bool RequiresRestart;

  // Overrides the label we display on the UI
  public string CustomLabel;

  // the initial value of the script's parameter
  protected object initial;

  private FieldInfo field;
  private PropertyInfo property;

  // the unique key for serialization
  protected string Key {
    get {
      return script + ":" + parameter;
    }
  }

  void Awake() {
    parameter = gameObject.name;

    // set the UI label
    Text text = transform.Find("label").GetComponent<Text>();
    text.text = CustomLabel == "" ? parameter : CustomLabel;
    if (RequiresRestart)
      text.text = text.text + "**";
  }

  protected virtual void Start() {
    try {
      this.field = getField();
      this.property = getProperty();
      this.initial = this.field != null ? this.field.GetValue(script) : this.property.GetValue(script);
    } catch (Exception e) {
      Debug.Log(String.Format("Problem with setting control: {0}.{1}", script.GetType(), parameter));
      Debug.LogException(e);
    }
  }

  /// <summary>
  /// Syncs the UI value to the script's property
  /// </summary>
  /// <param name="val"></param>
  protected virtual void Sync(object val) {

    //find the type
    Type type = field != null ? field.FieldType : property.PropertyType;

    // Convert.ChangeType does not handle conversion to nullable types
    // if the property type is nullable, we need to get the underlying type of the property
    var targetType = IsNullableType(type) ? Nullable.GetUnderlyingType(type) : type;

    // Returns an System.Object with the specified System.Type and whose value is
    // equivalent to the specified object.
    val = Convert.ChangeType(val, targetType);

    if (field != null)
      field.SetValue(this.script, val);
    else {;
      property.SetValue(this.script, val);
    }
  }
  /// <summary>
  /// checks if nullable for property reflection
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  private static bool IsNullableType(Type type) {
    return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
  }

  /// <summary>
  /// Gets the value from this UI control
  /// </summary>
  /// <returns></returns>
  protected abstract string GetValue();

  /// <summary>
  /// Loads the data from storage if it exists
  /// </summary>
  /// <param name="obj"></param>
  public void Load(Dictionary<string, string> data) {
    // no persisted data
    try {
      if (data != null && data.ContainsKey(Key)) {
        Sync(data[Key]);
        return;
      }
    } catch (Exception e) {
      Debug.Log("Issue loading data: " + e.ToString());
    }
    // we have no data or something went wrong, load initial
    Sync(initial);
  }

  /// <summary>
  /// Saves this control
  /// </summary>
  /// <param name="data"></param>
  public virtual void Save(Dictionary<string, string> data) {
    data.Add(Key, GetValue());
  }

  /// <summary>
  /// Uses reflection to get script field
  /// </summary>
  /// <returns></returns>
  protected FieldInfo getField() {
    Type type = script.GetType();
    return type.GetField(parameter);
  }

  /// <summary>
  /// Uses reflection to get script property
  /// </summary>
  /// <returns></returns>
  protected PropertyInfo getProperty() {
    Type type = script.GetType();
    return type.GetProperty(parameter);
  }
}