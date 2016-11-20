using UnityEngine;

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
