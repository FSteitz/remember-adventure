using UnityEngine;

/// <summary>
///
/// </summary>
public interface Translation {

  /// <summary>
  ///
  /// </summary>
  SystemLanguage GetLanguage();

  /// <summary>
  ///
  /// </summary>
	string GetTranslation(string key);
}
