namespace UGF.Services.Localization
{
    public static class TextService
    {
        public static Language CurrentLanguage => TextRepo.CurrentLanguage;

        public static void SetLanguage(Language language)
        {
            TextRepo.SetLanguage(language);
        }

        public static string HelloWorld()
        {
            return TextRepo.GetText(TextKey.HelloWorld);
        }
    }
}