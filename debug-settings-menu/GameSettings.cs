using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {
  public GameObject Settings;
  private SettingsModule[] settings;

  private string SettingsFilePathDirectory {
    get {
      return Application.streamingAssetsPath + "/Settings";
    }
  }
  private string SettingsFilePath {
    get {
      return SettingsFilePathDirectory + "/settings.json";
    }
  }

  void Awake() {
    settings = GetComponentsInChildren<SettingsModule>();
  }
  void Start() {
    // init director if not exists
    if (!Directory.Exists(SettingsFilePathDirectory)) {
      Directory.CreateDirectory(SettingsFilePathDirectory);
    }
    Reset();
  }

  public void ToggleSettings(bool? state = null) {
    if (state != null) {
      Settings.SetActive((bool) state);
    } else {
      Settings.SetActive(!Settings.activeInHierarchy);
    }
  }

  /// <summary>
  /// Saves current settings to disk
  /// </summary>
  public void Save() {
    Dictionary<string, string> data = new Dictionary<string, string>();
    foreach (var module in settings)
      data.AddAll(module.Save());

    // write to file
    string json = JsonUtility.ToJson(new GameSettingsRaw(data), prettyPrint : true);
    File.WriteAllText(SettingsFilePath, json);
  }

  /// <summary>
  /// Loads a settings file to disk
  /// </summary>
  public void Load() {
    // loads the file if it exists
    if (File.Exists(SettingsFilePath))
      Reset();
  }

  /// <summary>
  /// deletes current cached save
  /// </summary>
  public void ResetToDefault() {
    Reset(true);
  }

  /// <summary>
  /// Loads initial settings
  /// </summary>
  public void Reset(bool useDefaults = false) {
    // load initial
    var currentSettings = getCurrentSettingsFile();
    if (useDefaults)
      currentSettings = null;
    foreach (var module in settings) {
      module.Load(currentSettings);
    }
  }

  /// <summary>
  /// Gets the currentSettings file if it exists
  /// </summary>
  /// <returns></returns>
  private Dictionary<string, string> getCurrentSettingsFile() {
    if (File.Exists(SettingsFilePath)) {
      GameSettingsRaw raw = JsonUtility.FromJson<GameSettingsRaw>(File.ReadAllText(SettingsFilePath));
      if (raw != null) return raw.GetSettings();
    }
    return null;
  }
}