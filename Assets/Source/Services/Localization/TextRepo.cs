using System;
using System.Collections.Generic;
using UGF;
using UnityEngine;

namespace Source.Services.Localization
{
    public static class TextRepo
    {
        public static Language CurrentLanguage { get; private set; }

        static TextRepo()
        {
            var storedLanguage = PlayerPrefs.GetInt(UgfConstants.PREFS_KEY_LANGUAGE);

            var isStoredLanguageValid = Enum.IsDefined(typeof(Language), storedLanguage);
            if (isStoredLanguageValid)
            {
                CurrentLanguage = (Language)storedLanguage;
            }
            else
            {
                SetLanguage(Language.English);
            }
        }

        public static void SetLanguage(Language language)
        {
            PlayerPrefs.SetInt(UgfConstants.PREFS_KEY_LANGUAGE, (int)language);
            CurrentLanguage = language;
        }

        public static string GetText(TextKey textKey)
        {
            switch (CurrentLanguage)
            {
                case Language.English:
                    return _textsEN[textKey];

                case Language.German:
                    return _textsDE[textKey];

                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentLanguage), CurrentLanguage, null);
            }
        }

        public static string GetLanguageText(Language languageKey)
        {
            return _textsLANGUAGE[languageKey];
        }

        private static Dictionary<Language, string> _textsLANGUAGE = new Dictionary<Language, string>
        {
            { Language.English, "English"},
            { Language.German, "Deutsch"}
        };

        private static Dictionary<TextKey, string> _textsEN = new Dictionary<TextKey, string>
        {
            { TextKey.ApplicationName, "Runner in Unity ECS"},
            { TextKey.Play, "Play!"},
            { TextKey.Restart, "Restart"},
            { TextKey.YouDied, "You Died!"},
            { TextKey.Distance, "Distance traveled:"},
            { TextKey.BestDistance, "Best distance:"}
        };

        private static Dictionary<TextKey, string> _textsDE = new Dictionary<TextKey, string>
        {
            { TextKey.ApplicationName, "Runner in Unity ECS"},
            { TextKey.Play, "Spielen!"},
            { TextKey.Restart, "Neustart"},
            { TextKey.YouDied, "Gestorben!"},
            { TextKey.Distance, "Erreichte Distanz:"},
            { TextKey.BestDistance, "Beste Distanz:"}
        };
    }
}
