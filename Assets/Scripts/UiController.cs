using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class UiController : MonoBehaviour {

  public Text tryText;
  public Text winText;

  public Button restartButton;
  public Button quitButton;

  public bool IsWinDialogVisible { get; set; }

	/// <summary>
  ///
  /// </summary>
	void Start () {
    winText.text = TranslationProvider.Get(TranslationKey.TextWin);
    winText.gameObject.SetActive(false);
    quitButton.gameObject.SetActive(false);
    restartButton.gameObject.SetActive(false);
	}

	/// <summary>
  ///
  /// </summary>
	void Update () {
    if (IsWinDialogVisible && !winText.IsActive()) {
      restartButton.gameObject.SetActive(true);
      quitButton.gameObject.SetActive(true);
      winText.gameObject.SetActive(true);
    }
	}

  /// <summary>
  ///
  /// </summary>
  public void UpdateTryText(int tryCount) {
    tryText.text = String.Format(TranslationProvider.Get(TranslationKey.TextTries), tryCount);
  }
}
