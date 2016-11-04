using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Rotator : MonoBehaviour {

  private const float ROTATION_SPEED = 1.5f;

  public readonly Vector3 FORWARD = Vector3.forward;
  public readonly Vector3 BACK = Vector3.back;

  public bool HasStarted { get; set; }
  public bool HasFinished { get; set; }

  private Vector3 direction;
  private Vector3 targetAngle;
  private float maxAngle;

	/// <summary>
  ///
  /// </summary>
	void Start () {
    Reset();
	}

	/// <summary>
  ///
  /// </summary>
	void Update () {
    if (HasStarted && !HasFinished) {
      Rotate();
    }
	}

  /// <summary>
  ///
  /// </summary>
  public void Rotate(Vector3 direction, float maxAngle) {
    this.direction = direction;
    this.targetAngle = this.direction * maxAngle;
    this.maxAngle = maxAngle;

    HasStarted = true;
  }

  /// <summary>
  ///
  /// </summary>
  public void Reset() {
    HasFinished = false;
    HasStarted = false;

    direction = Vector3.zero;
  }

  /// <summary>
  ///
  /// </summary>
  private void Rotate() {
    Vector3 targetAngles = transform.eulerAngles + targetAngle;
    Vector3 eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, ROTATION_SPEED * Time.deltaTime);

    // Example: Max angle is 180 degrees
    //
    // When revealing, the z-axis increases (e.g. 19, 121, 177) and it should stop once it exceeds 180 degrees. In this
    // case, the z-axis is set to 180.
    //
    // When hiding, the z-axis decreases (e.g. 177, 121, 19) and it should stop once it exceeds 0 degrees, which is a
    // value greater than 180 degrees (e.g. 360). In this case, the z-axis is set to 0.
    if (eulerAngles.z > maxAngle) {
      eulerAngles.z = (direction == FORWARD) ? maxAngle : 0;
      HasStarted = false;
      HasFinished = true;
    }

    transform.eulerAngles = eulerAngles;
  }
}
