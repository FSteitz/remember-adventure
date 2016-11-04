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
  public bool IsRevealed { get; set; }

  private Rotator rotator;
	private bool reveal = false;
  private bool reset = false;
  private bool hasMatched = false;

  /// <summary>
  ///
  /// </summary>
  void Start() {
    rotator = gameObject.GetComponent<Rotator>();
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
  void OnMouseDown() {
    if (!hasMatched && !Board.HasToggledPair()) {
      reveal = true;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void MarkAsMatched() {
    hasMatched = true;
    Board.UnregisterToggledTile(gameObject);
    Destroy(gameObject); // TODO: Replace the destruction with an animation (and disable it at the end)
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
      rotator.Rotate(rotator.FORWARD, REVEALED_SIDE_ANGLE);
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
      rotator.Rotate(rotator.BACK, REVEALED_SIDE_ANGLE);
    }
  }
}
