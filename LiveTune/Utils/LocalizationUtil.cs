using LiveTune.Assets;
using System.Resources;

namespace LiveTune.Utils
{
    public class LocalizationUtil
    {
        static ResourceSet? ResourceSet { get; }
        static LocalizationUtil()
        {
            ResourceSet = Resources.ResourceManager.GetResourceSet(Resources.Culture, true, true);
        }
        public static string? GetDisplayName(string? key, string? defaultValue = "")
        {
            if (string.IsNullOrWhiteSpace(key)) return defaultValue;
            var value = Resources.ResourceManager.GetString(key, Resources.Culture);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}
