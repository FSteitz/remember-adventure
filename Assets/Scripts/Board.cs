﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Board : MonoBehaviour {

  private const int MATCH_COUNT = 2;

  public GameController GameController { get; set; }

  private List<GameObject> activeTiles = new List<GameObject>();
	private List<GameObject> toggledTiles = new List<GameObject>();
  private bool allMatch = true;

  /// <summary>
  ///
  /// </summary>
  void Start() {
    Board board = gameObject.GetComponent<Board>();

    foreach (Transform row in transform) {
      foreach (Transform tile in row) {
        tile.gameObject.GetComponent<RememberTile>().Board = board;
        activeTiles.Add(tile.gameObject);
      }
    }
  }

  /// <summary>
  ///
  /// </summary>
  public bool HasToggledPair() {
    return toggledTiles.Count == MATCH_COUNT;
  }

  /// <summary>
  ///
  /// </summary>
  public void RegisterToggledTile(GameObject tile) {
    if (!HasToggledPair()) {
      if (toggledTiles.Count > 0) {
        allMatch &= toggledTiles.Last().tag.Equals(tile.tag);
      }

      toggledTiles.Add(tile);
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void UnregisterToggledTile(GameObject tile) {
    toggledTiles.Remove(tile);

    if (toggledTiles.Count == 0) {
      allMatch = true;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void CheckForMatch() {
    if (HasToggledPair() && AllRevealed()) {
      if (allMatch) {
        MarkAllAsMatched();
      } else {
        ResetAll();
      }
    }
  }

  /// <summary>
  ///
  /// </summary>
  private bool AllRevealed() {
    bool allRevealed = false;

    foreach (GameObject tile in toggledTiles) {
      allRevealed = tile.GetComponent<RememberTile>().IsRevealed;

      if (!allRevealed) {
        break;
      }
    }

    return allRevealed;
  }

  /// <summary>
  ///
  /// </summary>
  private void MarkAllAsMatched() {
    new List<GameObject>(toggledTiles).ForEach(tile => {
      tile.GetComponent<RememberTile>().MarkAsMatched();
      activeTiles.Remove(tile);
    });

    if (activeTiles.Count == 0) {
      GameController.RegisterFinishedBoard(gameObject);
    }
  }

  /// <summary>
  ///
  /// </summary>
  private void ResetAll() {
    new List<GameObject>(toggledTiles).ForEach(tile => {
      tile.GetComponent<RememberTile>().Reset();
    });
  }
}
