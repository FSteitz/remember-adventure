using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

  private const string BOARD_TAG = "Board";

  public Text winText;
  public Button restartButton;
  public Button quitButton;

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

    winText.gameObject.SetActive(false);
    quitButton.gameObject.SetActive(false);
    restartButton.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
    if (allBoardsFinished && !winText.IsActive()) {
      restartButton.gameObject.SetActive(true);
      quitButton.gameObject.SetActive(true);
      winText.gameObject.SetActive(true);
    }
	}

  /// <summary>
  ///
  /// </summary>
  public void RegisterFinishedBoard(GameObject board) {
    activeBoards.Remove(board);
    allBoardsFinished = activeBoards.Count == 0;
  }

  /// <summary>
  ///
  /// </summary>
  public void RestartScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  /// <summary>
  ///
  /// </summary>
  public void QuitGame() {
    Application.Quit();
  }
}
