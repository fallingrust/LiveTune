using System.Collections.Generic;
using System.Globalization;

namespace LiveTune.Utils
{
    public class LocalUtil
    {
        static LocalUtil()
        {
            for (int i = 0; i < CountryCode.Length; i++)
            {
                ChineseCountry.Add(CountryCode[i], ChineseCountryName[i]);
                EnglishCountry.Add(CountryCode[i], EnglishCountryName[i]);
            }

            LocalCountry.Add("zh", ChineseCountry);
            LocalCountry.Add("en", EnglishCountry);

            for (int i = 0; i < LanguageCode.Length; i++)
            {
                ChineseLanguage.Add(LanguageCode[i], ChineseLanguageName[i]);
                EnglishLanguage.Add(LanguageCode[i], EnglishLanguageName[i]);
            }

            LocalLanguage.Add("zh", ChineseLanguage);
            LocalLanguage.Add("en", EnglishLanguage);
        }
        public static Dictionary<string, Dictionary<string, string>> LocalCountry { get; } = [];

        public static Dictionary<string, string> ChineseCountry { get; } = [];
        public static Dictionary<string, string> EnglishCountry { get; } = [];

        public static readonly string[] CountryCode = ["us", "de", "ru", "fr", "gr", "cn","gb"];
        public static readonly string[] ChineseCountryName = ["美国", "德国", "俄罗斯", "法国", "希腊", "中国","英国"];
        public static readonly string[] EnglishCountryName = ["United States", "United States", "Russia", "France", "Greece", "China", "United Kingdom"];




        public static Dictionary<string, Dictionary<string, string>> LocalLanguage { get; } = [];

        public static Dictionary<string, string> ChineseLanguage { get; } = [];
        public static Dictionary<string, string> EnglishLanguage { get; } = [];

        public static readonly string[] LanguageCode = ["english", "chinese", "italian"];
        public static readonly string[] ChineseLanguageName = ["英文", "中文", "意大利语"];
        public static readonly string[] EnglishLanguageName = ["English", "Chinese", "Italian"];




        public static string GetCountyDisplayName(string countryCode, string defaultValue = "")
        {
            if (LocalCountry.TryGetValue(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, out var contries)
                && contries.TryGetValue(countryCode.ToLower(), out var country))
            {
                return country;
            }

            return defaultValue;
        }



        public static string GetLanguageisplayName(string languageCode, string defaultValue = "")
        {
            if (LocalLanguage.TryGetValue(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, out var languages)
                && languages.TryGetValue(languageCode.ToLower(), out var language))
            {
                return language;
            }

            return defaultValue;
        }
    }
}
