using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
