using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class GermanTranslation : Translation {

	private readonly Dictionary<string, string> Translations = new Dictionary<string, string> {
    { TranslationKey.TextTries, "Versuche: {0}" },
    { TranslationKey.TextWin, "Erfolg!" }
  };

  /// <summary>
  ///
  /// </summary>
  public SystemLanguage GetLanguage() {
    return SystemLanguage.German;
  }

  /// <summary>
  ///
  /// </summary>
  public string Get(string key) {
    return Translations[key];
  }
}
