using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>
public class GameController : MonoBehaviour {

  public Text tryText;
  public Text winText;
  public Button restartButton;
  public Button quitButton;

  private List<GameObject> activeBoards = new List<GameObject>();
  private bool allBoardsFinished = false;

  /// <summary>
  ///
  /// </summary>
	void Start () {
    var controller = gameObject.GetComponent<GameController>();

    foreach (GameObject board in GameObject.FindGameObjectsWithTag(Tag.Board)) {
      board.GetComponent<Board>().GameController = controller;
      activeBoards.Add(board);
    }

    tryText.text = TranslationProvider.Get(TranslationKey.TextTries);
    winText.text = TranslationProvider.Get(TranslationKey.TextWin);

    winText.gameObject.SetActive(false);
    quitButton.gameObject.SetActive(false);
    restartButton.gameObject.SetActive(false);
	}

	/// <summary>
  ///
  /// </summary>
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
