using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

  private const string BOARD_TAG = "Board";

  public Text winText;

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

    winText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
    if (allBoardsFinished && !winText.enabled) {
      winText.enabled = true;
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
