using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  // Lets us control exactly how much each axis affected when moving with/against the stairs slope
  const float StairSlowDownXPos = 0.8f;
  const float StairSlowDownXNeg = 0.6f;
  const float StairSlowDownYPos = 0.8f;
  const float StairSlowDownYNeg = 0.6f;

  public Stack<Stairs> CurrentStairs = new Stack<Stairs>();
  /// <summary>
  /// Function that controls the actual movement
  /// </summary>
  /// <param name="direction"></param>
  /// <param name="speed"></param>
  public void Move(Vector2 direction, float speed) {
    Vector2 pos = transform.position;
    Vector2 newMovement = ApplyStairMovement(direction * speed);
    pos += newMovement;

    // set position
    transform.position = pos;
  }

  /// <summary>
  /// Converts a vector to conform to stairs angle
  /// </summary>
  /// <param name="movement"></param>
  /// <returns></returns>
  public Vector2 ApplyStairMovement(Vector2 movement) {

    // exit early
    if (Stairs.Count == 0) return movement;

    Stairs stairs = CurrentStairs.Peek();
    Vector2 stairsDirection = stairs.GetDirection();

    // apply slows for vertical direction
    movement.y *= (Mathf.Sign(stairsDirection.y) == Mathf.Sign(movement.y)) ? StairSlowDownYNeg : StairSlowDownYPos;
    float originalLength = movement.magnitude;

    float angle = stairs.Angle;
    bool isVertical = angle == 0;

    // since we are using the range 0-180, we need to do some clean up in the angle here
    // I'm sure there is a cleaner way to do this, but it works so whatever.
    bool isRight = angle > 90;
    if (isRight) {
      angle = angle - 90;
    } else {
      angle = 90 - angle;
    }
    // calculate tan, negate based on the angle because of math
    float tan = -Mathf.Tan(angle * Mathf.Deg2Rad);
    if (isRight) {
      tan *= -1;
    }
    // For vertical stairs we need to override this to 0 since it will increase y infinitely when our angle is 0
    if (isVertical)
      tan = 0;

    // SPECIFIC CASE: Player walks diagonally down stairs
    // This results in the player not moving in the y direction (cancels out due to tan angle)
    // we allow them to move a bit because even though its correct, looks weird.
    // This is a perfect example of not following exact realism for the sake of game-feel
    if (Mathf.Sign(stairsDirection.x) != Mathf.Sign(movement.x) && movement.y > 0) {
      tan /= 2;
    }
    // apply vector calc to y and normalize to maintain speed
    movement.y += movement.x * tan;
    movement = movement.normalized * originalLength;
    return movement;
  }
}