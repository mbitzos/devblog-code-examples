using System;
using System.Collections.Generic;

/// <summary>
/// Holds save data for a scene in the game
/// </summary>
[Serializable]
public class SceneData {

  public Dictionary<string, ObjectData> objects = new Dictionary<string, ObjectData>();

  /// <summary>
  /// Gets the object data for this scene
  /// </summary>
  /// <param name="objectId"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public object GetObjectData<T>(string objectId) {
    return objects.GetValueOrDefault(objectId, null);
  }
}