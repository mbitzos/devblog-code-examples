using UnityEngine;
/// <summary>
/// Models an object that defines its save/load behaviour
/// </summary>
public abstract class Saver : MonoBehaviour {

  [Tooltip("The order we want to save this object in, in the order from [-inf, inf].")]
  public int SavePriority = 0;
  [Tooltip("The order we want to load this object in, in the order from [-inf, inf].")]
  public int LoadPriority = 0;

  // The unique id used to uniquely identifying this object in the save data"
  public string SaveId {
    private set {
      this.name = value;
    }
    get {
      // TODO: Set this via editor manually, name is not a good id.
      return this.name;
    }
  }

  /// <summary>
  /// Returns the persisted save data for this object
  /// aka Serialization
  /// </summary>
  /// <returns></returns>
  public abstract ObjectData Save();

  /// <summary>
  /// Loads the object with the save data for this Saver
  /// aka Deserialization
  /// </summary>
  /// <param name="data"></param>
  public abstract void Load(ObjectData data);

}