using System;
using System.Collections.Generic;

using System.Text;
using UnityEngine;

namespace Coolfish.System
{
    public sealed class LocalizationInfo
    {
        public static readonly List<string> SupportLanguages = new List<string>()
        {
            "en",
            "zh_cn",
        };

        public static string LANGUAGE_PREFS_KEY = "LanguageCode";
        public static string s_langCode;
        public static string language
        {
            set
            {
                if (!SupportLanguages.Contains(value))
                    throw new ArgumentException("Non support language: " + value);

                s_langCode = value;
                PlayerPrefs.SetString(LANGUAGE_PREFS_KEY, s_langCode);
                PlayerPrefs.Save();
            }

            get
            {
                if (s_langCode == null)
                {
                    s_langCode = PlayerPrefs.GetString(LANGUAGE_PREFS_KEY);
                    if (s_langCode == null)
                    {
                        s_languageDict.TryGetValue(Application.systemLanguage, out s_langCode);
                    }

                    if (!SupportLanguages.Contains(s_langCode))
                    {
                        s_langCode = "en";
                    }
                }

                return s_langCode;
            }
        }

        private static Dictionary<SystemLanguage, string> s_languageDict = new Dictionary<SystemLanguage, string>()
        {
            { SystemLanguage.Afrikaans, "af" },
            { SystemLanguage.Arabic, "ar" },
            { SystemLanguage.Basque, "eu" },
            { SystemLanguage.Belarusian, "be" },
            { SystemLanguage.Bulgarian, "bg" },
            { SystemLanguage.Catalan, "ca" },
            { SystemLanguage.Chinese, "zh" },
            { SystemLanguage.Czech, "cs" },
            { SystemLanguage.Danish, "da" },
            { SystemLanguage.Dutch, "nl" },
            { SystemLanguage.English, "en" },
            { SystemLanguage.Estonian, "et" },
            { SystemLanguage.Faroese, "fo" },
            { SystemLanguage.Finnish, "fi" },
            { SystemLanguage.French, "fr" },
            { SystemLanguage.German, "de" },
            { SystemLanguage.Greek, "el" },
            { SystemLanguage.Hebrew, "he" },
            { SystemLanguage.Icelandic, "is" },
            { SystemLanguage.Indonesian, "id" },
            { SystemLanguage.Italian, "it" },
            { SystemLanguage.Japanese, "ja" },
            { SystemLanguage.Korean, "ko" },
            { SystemLanguage.Latvian, "lv" },
            { SystemLanguage.Lithuanian, "lt" },
            { SystemLanguage.Norwegian, "no" },
            { SystemLanguage.Portuguese, "pt" },
            { SystemLanguage.Romanian, "ro" },
            { SystemLanguage.Russian, "ru" },
            { SystemLanguage.SerboCroatian, "hr" }, //not sure
            { SystemLanguage.Slovak, "sk" },
            { SystemLanguage.Slovenian, "sl" },
            { SystemLanguage.Spanish, "es" },
            { SystemLanguage.Swedish, "sv" },
            { SystemLanguage.Thai, "th" },
            { SystemLanguage.Turkish, "tr" },
            { SystemLanguage.Ukrainian, "uk" },
            { SystemLanguage.Vietnamese, "vi" },
            { SystemLanguage.ChineseSimplified, "zh_cn" },
            { SystemLanguage.ChineseTraditional, "zh_tw" },
            { SystemLanguage.Unknown, "un" },
            { SystemLanguage.Hungarian, "hu" },
        };
    }
}
