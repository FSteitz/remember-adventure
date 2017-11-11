using System;
using UnityEngine;
using UnityEngine.UI;

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
