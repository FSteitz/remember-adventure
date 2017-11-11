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
public class Mover : MonoBehaviour {

  private const float DefaultSpeed = 1f;

  public bool IsMoving { get; set; }

  public float Speed { get; set; }

  private Vector3 targetPosition;

  /// <summary>
  ///
  /// </summary>
  void Awake() {
    targetPosition = Vector3.zero;
    Speed = DefaultSpeed;
    IsMoving = false;
  }

  /// <summary>
  ///
  /// </summary>
	void Update () {
    if (IsMoving) {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

      if (transform.position.y == targetPosition.y) {
        targetPosition = Vector3.zero;
        IsMoving = false;
      }
    }
	}

  /// <summary>
  ///
  /// </summary>
  public void MoveTo(Vector3 targetPosition) {
    this.targetPosition = targetPosition;
    IsMoving = true;
  }
}
