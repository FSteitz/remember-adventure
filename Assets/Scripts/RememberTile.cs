using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class RememberTile : MonoBehaviour {

  private const int REVEALED_SIDE_ANGLE = 180;
  private const int Z_AXIS_ROTATION_SPEED = 150;

	private bool clicked = false;
  private bool revealed = false;

  /// <summary>
  ///
  /// </summary>
	void Update () {
    if (clicked && !revealed) {
      float zAngle = Mathf.Round(transform.eulerAngles.z);

      if (zAngle < REVEALED_SIDE_ANGLE) {
        transform.Rotate(0, 0, Z_AXIS_ROTATION_SPEED * Time.deltaTime);
      } else {
        Vector3 angles = transform.eulerAngles;

        angles.z = REVEALED_SIDE_ANGLE;
        transform.eulerAngles = angles;
        revealed = true;
      }
    }
  }

  /// <summary>
  ///
  /// </summary>
  void OnMouseDown() {
    clicked = true;
  }
}
