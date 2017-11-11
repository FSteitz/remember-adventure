using UnityEngine;

/// Copyright 2016 Florian Steitz
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///   http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/// <summary>
///
/// </summary>
public class Rotator : MonoBehaviour {

  private const float RotationSpeed = 1.5f;

  public static readonly Vector3 Forward = Vector3.forward;
  public static readonly Vector3 Back = Vector3.back;

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
    var targetAngles = transform.eulerAngles + targetAngle;
    var eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, RotationSpeed * Time.deltaTime);

    // Example: Max angle is 180 degrees
    //
    // When revealing, the z-axis increases (e.g. 19, 121, 177) and it should stop once it exceeds 180 degrees. In this
    // case, the z-axis is set to 180.
    //
    // When hiding, the z-axis decreases (e.g. 177, 121, 19) and it should stop once it exceeds 0 degrees, which is a
    // value greater than 180 degrees (e.g. 360). In this case, the z-axis is set to 0.
    if (eulerAngles.z > maxAngle) {
      eulerAngles.z = (direction == Forward) ? maxAngle : 0;
      HasStarted = false;
      HasFinished = true;
    }

    transform.eulerAngles = eulerAngles;
  }
}
