using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager for saving and loading game data
/// This currently uses PlayerPrefs for storing the current save file, this would most likely be migrated to a different system later.
/// NOTE: We could utilize instead a pub/sub style of saving, but this is simpler for now and allows us to have sort priority handled in the manager.
/// NOTE: This manager also handles UI for saving and loading, which should be decoupled later.
/// NOTE: This manager has coroutines as a first class citizen by ensuring all file IO is kept off the main thread and yielded in the main enumerator.
/// </summary>
public class SaveManager : MonoBehaviour {

  const string SAVE_EXTENSION = ".save";
  const string LAST_SAVE_KEY = "lastsave";

  /// <summary>
  /// get all savers in the scene in order
  /// NOTE: We could easily change this to instead have savers subscribe themselves to the SaveManager
  /// </summary>
  /// <returns></returns>
  private Saver[] getAllSavers() {
    return UnityEngine.Object.FindObjectsOfType<Saver>();
  }

  /// <summary>
  /// Saves the current scene
  /// NOTE: This combines new save create and saving, which should eventually be decoupled
  /// </summary>
  public IEnumerator Save() {
    Debug.Log("Saving...");
    Saver[] savers = getAllSavers();
    SaveData existingSave = null;
    yield return getOrCreateExistingSaveData(s => { existingSave = s; });

    // save all objects to existing save in their own tasks
    SceneData sceneData = new SceneData();
    savers.ToList().ForEach(saver => {
      sceneData.objects[saver.SaveId] = saver.Save();
    });
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    existingSave.scenes[currentSceneIndex] = sceneData;
    yield return saveFile(existingSave);
    Debug.Log("Saving completed.");
  }

  /// <summary>
  /// Loads the latest save file, throws an exception if none exists
  /// </summary>
  public IEnumerator LoadLatest() {
    if (!HasLastSaveFile()) {
      throw new MissingSaveFileException("No save file exists");
    }
    yield return Load(PlayerPrefs.GetString(LAST_SAVE_KEY));
  }

  /// <summary>
  /// Loads the save file for the current scene 
  /// </summary>
  public IEnumerator Load(string saveId) {
    Debug.Log($"Loading save {saveId}");
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SaveData saveData = null;
    yield return fetchSaveFile(saveId, s => { saveData = s; });
    if (!saveData.scenes.ContainsKey(currentSceneIndex)) {
      yield break;
    }
    SceneData sceneData = saveData.scenes[currentSceneIndex];

    // load persisted data into savers
    getAllSavers()
      .OrderBy(saver => saver.LoadPriority).ToList()
      .ForEach((Saver saver) => {
        Debug.Log(sceneData.objects);
        if (sceneData.objects.ContainsKey(saver.SaveId)) {
          saver.Load(sceneData.objects[saver.SaveId]);
        };
      });
    Debug.Log("Loading completed.");
  }

  /// <summary>
  /// Save data to file
  /// </summary>
  private IEnumerator saveFile(SaveData data) {
    // set the current scene
    string jsonSaveData = JsonConvert.SerializeObject(data);
    string filepath = (Application.persistentDataPath + "/" + data.ID + SAVE_EXTENSION);
    yield return TaskUtils.YieldTask(File.WriteAllTextAsync(filepath, jsonSaveData));

    // currently uses player prefs...might change
    PlayerPrefs.SetString(LAST_SAVE_KEY, data.ID);
    PlayerPrefs.Save();
  }

  /// <summary>
  /// If the game has an existing game file
  /// </summary>
  /// <returns></returns>
  public bool HasLastSaveFile() {
    return PlayerPrefs.HasKey(LAST_SAVE_KEY);
  }

  /// <summary>
  /// Loads the last saved file, if none exists, create one
  /// </summary>
  public IEnumerator getOrCreateExistingSaveData(Action<SaveData> callback) {
    if (HasLastSaveFile()) {
      yield return fetchSaveFile(PlayerPrefs.GetString(LAST_SAVE_KEY), callback);
    } else {
      yield return null;
      callback(new SaveData());
    }
  }

  /// <summary>
  /// fetch the save file
  /// </summary>
  /// <param name="id">The ID of the load file</param>
  private IEnumerator fetchSaveFile(string id, Action<SaveData> callback) {
    // check if exists
    string saveFilePath = Application.persistentDataPath + "/" + id + SAVE_EXTENSION;
    Debug.Log($"Fetching save file {saveFilePath}");
    if (!File.Exists(saveFilePath)) {
      throw new MissingSaveFileException("No save file exists");
    }
    Task<string> task = File.ReadAllTextAsync(saveFilePath);
    yield return new WaitUntil(() => task.IsCompleted);
    SaveData data = JsonConvert.DeserializeObject<SaveData>(task.Result);
    callback(data);
  }
}