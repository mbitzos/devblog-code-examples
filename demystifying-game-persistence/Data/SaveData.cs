using System;
using System.Collections.Generic;

/// <summary>
/// Holds the save data for the game
/// </summary>
[Serializable]
public class SaveData {

  /// <summary>
  /// Scene specific data
  /// </summary>
  public Dictionary<int, SceneData> scenes = new Dictionary<int, SceneData>();
  public string ID;

  public SaveData() {
    ID = Guid.NewGuid().ToString();
  }

  /// <summary>
  /// Gets the scene data for this save file 
  /// </summary>
  /// <param name="sceneName"></param>
  /// <param name="objectId"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public object GetSceneObjectData<T>(int sceneIndex, string objectId) {
    return scenes.GetValueOrDefault(sceneIndex, null)?.GetObjectData<T>(objectId);
  }
}