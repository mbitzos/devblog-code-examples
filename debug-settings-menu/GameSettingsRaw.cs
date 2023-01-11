using System;
using System.Collections.Generic;

[Serializable]
public class GameSettingsRaw {
  public List<GameSettingsKVP> settings = new List<GameSettingsKVP>();

  public GameSettingsRaw(Dictionary<string, string> settings) {
    foreach (KeyValuePair<string, string> kvp in settings) {
      this.settings.Add(new GameSettingsKVP { Key = kvp.Key, Value = kvp.Value });
    }
  }

  // converts this to settings
  public Dictionary<string, string> GetSettings() {
    Dictionary<string, string> data = new Dictionary<string, string>();
    foreach (GameSettingsKVP kvp in settings) {
      data.Add(kvp.Key, kvp.Value);
    }
    return data;
  }
}

[Serializable]
public class GameSettingsKVP {
  public string Key;
  public string Value;
}