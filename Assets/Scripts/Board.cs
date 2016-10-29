using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///
/// </summary>
public class Board : MonoBehaviour {

	GameObject firstRevealedTile;
  GameObject secondRevealedTile;

  /// <summary>
  ///
  /// </summary>
  public bool HasRevealedPair() {
    return firstRevealedTile != null && secondRevealedTile != null;
  }

  /// <summary>
  ///
  /// </summary>
  public void RegisterRevealedTile(GameObject tile) {
    if (firstRevealedTile == null) {
      firstRevealedTile = tile;
    } else if (secondRevealedTile == null) {
      secondRevealedTile = tile;
    }

    if (firstRevealedTile != null && secondRevealedTile != null) {
      if (firstRevealedTile.tag.Equals(secondRevealedTile.tag)) {
        firstRevealedTile.GetComponent<RememberTile>().MarkMatched();
        firstRevealedTile = null;

        secondRevealedTile.GetComponent<RememberTile>().MarkMatched();
        secondRevealedTile = null;
      }
    }
  }
}
