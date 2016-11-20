using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class EnglishTranslation : Translation {

  private readonly Dictionary<string, string> Translations = new Dictionary<string, string> {
    { TranslationKey.TextTries, "Tries: {0}" },
    { TranslationKey.TextWin, "Success!" }
  };

  /// <summary>
  ///
  /// </summary>
  public SystemLanguage GetLanguage() {
    return SystemLanguage.English;
  }

  /// <summary>
  ///
  /// </summary>
  public string Get(string key) {
    return Translations[key];
  }
}
