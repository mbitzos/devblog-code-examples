using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds save data for a game object in a scene in a game
/// </summary>
[Serializable]
public class ObjectData {

  public Dictionary<string, object> properties = new Dictionary<string, object>();

  public ObjectData(Dictionary<string, object> properties) {
    this.properties = properties;
  }

  /// <summary>
  /// Gets the property from this object data
  /// </summary>
  /// <param name="propertyName"></param>
  /// <param name="defaultValue"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public T Get<T>(string propertyName, T defaultValue) {
    return (T) properties.GetValueOrDefault(propertyName, defaultValue);
  }
}