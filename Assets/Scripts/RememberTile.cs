using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class RememberTile : MonoBehaviour {

  private const float REVEALED_SIDE_ANGLE = 180f;
  private const float ROTATION_SPEED = 1.5f;

  private readonly Vector3 FORWARD = Vector3.forward * REVEALED_SIDE_ANGLE;
  private readonly Vector3 BACKWARD = Vector3.back * REVEALED_SIDE_ANGLE;

  public Board Board { get; set; }

	private bool reveal = false;
  private bool revealed = false;
  private bool registered = false;
  private bool matched = false;
  private bool reset = false;

  /// <summary>
  ///
  /// </summary>
	void Update () {
    if (reveal && !revealed) {
      if (!registered) {
        Board.Register(transform.gameObject);
        registered = true;
      }

      if (Rotate(FORWARD)) {
        Board.CheckForMatch();
        registered = false;
        reveal = false;
      }
    } else if (reset && revealed) {
      if (Rotate(BACKWARD)) {
        Board.Unregister(transform.gameObject);
        matched = false;
        reset = false;
      }
    }
  }

  /// <summary>
  ///
  /// </summary>
  void OnMouseDown() {
    if (!matched && !Board.HasRevealedPair()) {
      reveal = true;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public bool IsRevealed() {
    return revealed;
  }

  /// <summary>
  ///
  /// </summary>
  public void MarkAsMatched() {
    matched = true;
    Board.Unregister(transform.gameObject);
    Destroy(transform.gameObject); // TODO: Replace the destruction with an animation (and disable it at the end)
  }

  /// <summary>
  ///
  /// </summary>
  public void Reset() {
    reset = true;
  }

  /// <summary>
  ///
  /// </summary>
  private bool Rotate(Vector3 direction) {
    Vector3 targetAngles = transform.eulerAngles + direction;
    Vector3 eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, ROTATION_SPEED * Time.deltaTime);
    bool reachedTarget = false;

    // When revealing, the z-axis increases (e.g. 19, 121, 177) and it should stop once it exceeds 180 degrees. In this
    // case, the z-axis is set to 180.
    //
    // When hiding, the z-axis decreases (e.g. 177, 121, 19) and it should stop once it exceeds 0 degrees, which is a
    // value greater than 180 degrees (e.g. 360). In this case, the z-axis is set to 0.
    if (eulerAngles.z > REVEALED_SIDE_ANGLE) {
      eulerAngles.z = (direction == FORWARD) ? REVEALED_SIDE_ANGLE : 0;
      revealed = !revealed;
      reachedTarget = true;
    }

    transform.eulerAngles = eulerAngles;
    return reachedTarget;
  }
}
