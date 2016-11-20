using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///
/// </summary>
public class TranslationProvider {

  private static Translation activeTranslation;

  /// <summary>
  ///
  /// </summary>
	static TranslationProvider() {
    var availableTranslations = new Dictionary<SystemLanguage,Translation>();
    var translationTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && typeof(Translation).IsAssignableFrom(type));

    foreach (var translationType in translationTypes) {
      var translation = (Translation) Activator.CreateInstance(translationType);
      var language = translation.GetLanguage();

      if (language == Application.systemLanguage) {
        activeTranslation = translation;
      }

      availableTranslations[language] = translation;
    }

    if (activeTranslation == null && availableTranslations.Count() > 0) {
      activeTranslation = availableTranslations[SystemLanguage.English];
    }
  }

  /// <summary>
  ///
  /// </summary>
  public static string Get(String key) {
    return activeTranslation.GetTranslation(key);
  }
}
