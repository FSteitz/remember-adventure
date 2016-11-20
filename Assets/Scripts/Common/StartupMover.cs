using UnityEngine;

/// <summary>
///
/// </summary>
public class StartupMover : MonoBehaviour {

  public Vector3 position;

  public bool moveToX;
  public bool moveToY;
  public bool moveToZ;

  /// <summary>
  ///
  /// </summary>
	void Start () {
    if (!moveToX) {
      position.x = transform.position.x;
    }

    if (!moveToY) {
      position.y = transform.position.y;
    }

    if (!moveToZ) {
      position.z = transform.position.z;
    }

    gameObject.GetComponent<Mover>().MoveTo(position);
	}
}
