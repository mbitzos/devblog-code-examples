using Player;
using UnityEngine;

/// <summary>
/// Models a set of stairs that slows down the player when moving in direction
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Stairs : MonoBehaviour {

  [Range(0, 180)]
  public float Angle;

  /// <summary>
  /// Gets the direction of the stairs vector
  /// </summary>
  /// <returns></returns>
  public Vector2 GetDirection() {
    return Quaternion.AngleAxis(Angle, Vector3.forward) * Vector2.up;
  }

  /// <summary>
  /// Draws a line for us to easily tell which angle we are using for the horizontal stairs
  /// </summary>
  void OnDrawGizmosSelected() {
    Gizmos.color = Color.black;
    Vector2 direction = GetDirection();
    Vector2 origin = transform.position;
    Vector2 start = origin - direction.normalized * 0.5f;
    Vector2 end = origin + direction.normalized * 0.5f;
    Gizmos.DrawSphere(start, 0.03f);
    Gizmos.DrawSphere(end, 0.03f);
    Gizmos.DrawLine(start, end);
  }

  // Add settings
  void OnTriggerEnter2D(Collider2D other) {
    PlayerController player = other.gameObject.GetComponent<PlayerController>();
    if (player) {
      player.Stairs.Push(this);
    }
  }

  // Take away settings
  void OnTriggerExit2D(Collider2D other) {
    PlayerController player = other.gameObject.GetComponent<PlayerController>();
    if (player) {
      player.Stairs.Pop();
    }
  }

}