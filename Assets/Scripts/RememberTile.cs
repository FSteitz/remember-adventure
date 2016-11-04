using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class RememberTile : MonoBehaviour {

  private const float REVEALED_SIDE_ANGLE = 180f;

  public Board Board { get; set; }

  private Rotator rotator;
	private bool reveal = false;
  private bool revealed = false;
  private bool matched = false;
  private bool reset = false;

  /// <summary>
  ///
  /// </summary>
  void Start() {
    rotator = gameObject.GetComponent<Rotator>();
  }

  /// <summary>
  ///
  /// </summary>
	void Update () {
    if (reveal && !revealed) {
      Reveal();
    } else if (reset && revealed) {
      Hide();
    }
  }

  /// <summary>
  ///
  /// </summary>
  void OnMouseDown() {
    if (!matched && !Board.HasToggledPair()) {
      reveal = true;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public bool IsRevealed() {
    return revealed;
  }

  /// <summary>
  ///
  /// </summary>
  public void MarkAsMatched() {
    matched = true;
    Board.UnregisterToggledTile(transform.gameObject);
    Destroy(transform.gameObject); // TODO: Replace the destruction with an animation (and disable it at the end)
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
      revealed = true;
      reveal = false;

      Board.CheckForMatch();
      rotator.Reset();
    } else if (!rotator.HasStarted) {
      Board.RegisterToggledTile(transform.gameObject);
      rotator.Rotate(rotator.FORWARD, REVEALED_SIDE_ANGLE);
    }
  }

  /// <summary>
  ///
  /// </summary>
  private void Hide() {
    if (rotator.HasFinished) {
      Board.UnregisterToggledTile(transform.gameObject);
      rotator.Reset();

      revealed = false;
      matched = false;
      reset = false;
    } else if (!rotator.HasStarted) {
      rotator.Rotate(rotator.BACK, REVEALED_SIDE_ANGLE);
    }
  }
}
