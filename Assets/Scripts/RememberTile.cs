using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class RememberTile : MonoBehaviour {

  private const int REVEALED_SIDE_ANGLE = 180;
  private const int Z_AXIS_ROTATION_SPEED = 150;

  private Board board;

	private bool clicked = false;
  private bool revealed = false;
  private bool matched = false;
  private bool reset = false;

  /// <summary>
  ///
  /// </summary>
  void Start() {
    // The parent of this game object is a row and the row's parent is the board
    board = transform.parent.transform.parent.gameObject.GetComponent<Board>();
  }

  /// <summary>
  ///
  /// </summary>
	void Update () {
    if (clicked && !revealed) {
      if (Rotate(REVEALED_SIDE_ANGLE)) {
        board.RegisterRevealedTile(transform.gameObject);
      }
    } else if (reset && revealed) {
      Rotate(0);
    }
  }

  /// <summary>
  ///
  /// </summary>
  void OnMouseDown() {
    if (!matched && !board.HasRevealedPair()) {
      clicked = true;
      reset = false;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void MarkMatched() {
    matched = true;
    Destroy(transform.gameObject); // TODO: Replace the destruction with an animation (and disable it at the end)
  }

  /// <summary>
  ///
  /// </summary>
  public void Reset() {
    clicked = false;
    reset = true;
  }

  /// <summary>
  ///
  /// </summary>
  private bool Rotate(int targetSideAngle) {
    float zAngle = Mathf.Round(transform.eulerAngles.z);
    int rotationSpeed = Z_AXIS_ROTATION_SPEED;
    bool reachedTarget = false;
    bool mustRotate = false;

    if (targetSideAngle == 0) {
      mustRotate = zAngle > targetSideAngle;
      rotationSpeed *= -1;
    } else {
      mustRotate = zAngle < targetSideAngle;
    }

    if (mustRotate) {
      transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    } else {
      Vector3 angles = transform.eulerAngles;

      angles.z = targetSideAngle;
      transform.eulerAngles = angles;
      revealed = !revealed;
      reachedTarget = true;
    }

    return reachedTarget;
  }
}
