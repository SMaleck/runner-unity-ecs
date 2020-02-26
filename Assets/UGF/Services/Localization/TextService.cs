using System;

namespace UGF.Services.Localization
{
    public static class TextService
    {
        public static Language CurrentLanguage => TextRepo.CurrentLanguage;

        public static void SetLanguage(Language language)
        {
            TextRepo.SetLanguage(language);
        }

        public static string ApplicationName()
        {
            return TextRepo.GetText(TextKey.ApplicationName);
        }

        public static string Play()
        {
            return TextRepo.GetText(TextKey.Play);
        }

        public static string Restart()
        {
            return TextRepo.GetText(TextKey.Restart);
        }

        public static string AmountMeters(float amount)
        {
            return AmountMeters((int)Math.Round(amount));
        }

        public static string AmountMeters(int amount)
        {
            return $"{amount}m";
        }
    }
}