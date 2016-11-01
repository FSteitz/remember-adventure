using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Board : MonoBehaviour {

  private const int MATCH_COUNT = 2;

	private List<GameObject> revealedTiles = new List<GameObject>();

  /// <summary>
  ///
  /// </summary>
  public bool HasRevealedPair() {
    return revealedTiles.Count == MATCH_COUNT;
  }

  /// <summary>
  ///
  /// </summary>
  public void Register(GameObject tile) {
    if (!HasRevealedPair()) {
      revealedTiles.Add(tile);
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void Unregister(GameObject tile) {
    revealedTiles.Remove(tile);
  }

  /// <summary>
  ///
  /// </summary>
  public void CheckMatch() {
    if (HasRevealedPair() && AllRevealed()) {
      if (AllMatch()) {
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

    foreach (GameObject tile in revealedTiles) {
      allRevealed = tile.GetComponent<RememberTile>().IsRevealed();

      if (!allRevealed) {
        break;
      }
    }

    return allRevealed;
  }

  /// <summary>
  ///
  /// </summary>
  private bool AllMatch() {
    GameObject previousTile = null;
    bool allMatch = false;

    foreach (GameObject tile in revealedTiles) {
      if (previousTile != null) {
        allMatch = tile.tag.Equals(previousTile.tag);

        if (!allMatch) {
          break;
        }
      }

      previousTile = tile;
    }

    return allMatch;
  }

  /// <summary>
  ///
  /// </summary>
  private void MarkAllAsMatched() {
    new List<GameObject>(revealedTiles).ForEach(tile => {
      tile.GetComponent<RememberTile>().MarkMatched();
    });
  }

  /// <summary>
  ///
  /// </summary>
  private void ResetAll() {
    new List<GameObject>(revealedTiles).ForEach(tile => {
      tile.GetComponent<RememberTile>().Reset();
    });
  }
}
