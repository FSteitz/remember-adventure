using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Board : MonoBehaviour {

  private const int MATCH_COUNT = 2;

	private List<GameObject> revealedTiles = new List<GameObject>();
  private bool allMatch = true;

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
      if (revealedTiles.Count > 0) {
        allMatch &= revealedTiles.Last().tag.Equals(tile.tag);
      }

      revealedTiles.Add(tile);
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void Unregister(GameObject tile) {
    revealedTiles.Remove(tile);

    if (revealedTiles.Count == 0) {
      allMatch = true;
    }
  }

  /// <summary>
  ///
  /// </summary>
  public void CheckMatch() {
    if (HasRevealedPair() && AllRevealed()) {
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
