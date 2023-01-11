using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class GameSettingsEditorScripts {

  /// <summary>
  /// Syncs settings label in editor
  /// Just nice because it syncs during runtime, but can be confusing when editing
  /// </summary>
  [MenuItem("BeatBash/Sync Settings Labels")]
  static void SyncSettingsLabel() {

    SettingsControl[] settings = Camera.main.GetComponentsInChildren<SettingsControl>();
    foreach (SettingsControl control in settings) {
      Transform label = control.transform.Find("label");
      string labelText = control.CustomLabel != "" ? control.CustomLabel : control.gameObject.name;
      Text text = label.GetComponent<Text>();
      text.text = labelText;
      EditorUtility.SetDirty(text);
    }
  }
}