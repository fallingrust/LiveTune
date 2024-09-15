using LiveTune.Assets;

namespace LiveTune.Utils
{
    public class LocalizationUtil
    {   
        public static string? GetDisplayName(string? key, string? defaultValue = "")
        {
            if(string.IsNullOrWhiteSpace(key)) return defaultValue;
            var value = Resources.ResourceManager.GetString(key, Resources.Culture);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}
