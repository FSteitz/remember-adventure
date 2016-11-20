using UnityEngine;

/// <summary>
///
/// </summary>
public class RememberTile : MonoBehaviour {

  private const float REVEALED_SIDE_ANGLE = 180f;

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
      rotator.Rotate(Rotator.Forward, REVEALED_SIDE_ANGLE);
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
      rotator.Rotate(Rotator.Back, REVEALED_SIDE_ANGLE);
    }
  }
}
