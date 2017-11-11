using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// Copyright 2016 Florian Steitz
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///   http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
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
    return activeTranslation.Get(key);
  }
}
