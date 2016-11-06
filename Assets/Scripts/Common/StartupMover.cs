using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class StartupMover : MonoBehaviour {

  public Vector3 position;

  /// <summary>
  ///
  /// </summary>
	void Start () {
    gameObject.GetComponent<Mover>().MoveTo(position);
	}
}
