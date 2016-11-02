using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

  private const string BOARD_TAG = "Board";

  /// <summary>
  ///
  /// </summary>
	void Start () {
    GameController controller = gameObject.GetComponent<GameController>();

    foreach (GameObject board in GameObject.FindGameObjectsWithTag(BOARD_TAG)) {
      board.GetComponent<Board>().GameController = controller;
    }
	}

	// Update is called once per frame
	void Update () {

	}
}
