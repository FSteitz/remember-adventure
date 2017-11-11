using UnityEngine;

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
public class RememberTile : MonoBehaviour {

  private const float RevealedSideAngle = 180f;

  public Board Board { get; set; }
  public bool IsRevealed { get; set; }

  private Rotator rotator;
  private Mover mover;

	private bool reveal = false;
  private bool reset = false;
  private bool hasMatched = false;

  private Vector3 positionOrigin;

  /// <summary>
  ///
  /// </summary>
  void Start() {
    rotator = gameObject.GetComponent<Rotator>();
    mover = gameObject.GetComponent<Mover>();
    positionOrigin = transform.position;
    IsRevealed = false;
  }

  /// <summary>
  ///
  /// </summary>
	void Update () {
    if (reveal && !IsRevealed) {
      Reveal();
    } else if (reset && IsRevealed) {
      Hide();
    }
  }

  /// <summary>
  ///
  /// </summary>
  void OnMouseUp() {
    if (!hasMatched && !Board.HasToggledPair()) {
      reveal = true;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void MarkAsMatched() {
    var targetPosition = transform.position;

    hasMatched = true;
    Board.UnregisterToggledTile(gameObject);

    targetPosition.y = positionOrigin.y;
    mover.MoveTo(targetPosition);
  }

  /// <summary>
  ///
  /// </summary>
  public void Reset() {
    reset = true;
  }

  /// <summary>
  ///
  /// </summary>
  private void Reveal() {
    if (rotator.HasFinished) {
      IsRevealed = true;
      reveal = false;

      Board.CheckForMatch();
      rotator.Reset();
    } else if (!rotator.HasStarted) {
      Board.RegisterToggledTile(gameObject);
      rotator.Rotate(Rotator.Forward, RevealedSideAngle);
    }
  }

  /// <summary>
  ///
  /// </summary>
  private void Hide() {
    if (rotator.HasFinished) {
      Board.UnregisterToggledTile(gameObject);
      rotator.Reset();

      IsRevealed = false;
      hasMatched = false;
      reset = false;
    } else if (!rotator.HasStarted) {
      rotator.Rotate(Rotator.Back, RevealedSideAngle);
    }
  }
}
