using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

  private UiController uiController;
  private List<GameObject> activeBoards = new List<GameObject>();
  private bool allBoardsFinished = false;
  private int tryCount = 0;

  /// <summary>
  ///
  /// </summary>
	void Start () {
    var gameController = gameObject.GetComponent<GameController>();

    foreach (GameObject board in GameObject.FindGameObjectsWithTag(Tag.Board)) {
      board.GetComponent<Board>().GameController = gameController;
      activeBoards.Add(board);
    }

    uiController = GameObject.FindWithTag(Tag.UiController).GetComponent<UiController>();
    uiController.UpdateTryText(tryCount);
	}

	/// <summary>
  ///
  /// </summary>
	void Update () {
    if (allBoardsFinished && !uiController.IsWinDialogVisible) {
      uiController.IsWinDialogVisible = true;
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
