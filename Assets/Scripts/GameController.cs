using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

  private const string BOARD_TAG = "Board";

  private List<GameObject> activeBoards = new List<GameObject>();
  private bool allBoardsFinished = false;

  /// <summary>
  ///
  /// </summary>
	void Start () {
    GameController controller = gameObject.GetComponent<GameController>();

    foreach (GameObject board in GameObject.FindGameObjectsWithTag(BOARD_TAG)) {
      board.GetComponent<Board>().GameController = controller;
      activeBoards.Add(board);
    }
	}

	// Update is called once per frame
	void Update () {
    if (allBoardsFinished) {
      Debug.Log("Game over");
    }
	}

  /// <summary>
  ///
  /// </summary>
  public void RegisterFinishedBoard(GameObject board) {
    activeBoards.Remove(board);
    allBoardsFinished = activeBoards.Count == 0;
  }
}
