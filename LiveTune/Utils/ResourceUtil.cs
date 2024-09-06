using Avalonia;
using Avalonia.Media;

namespace LiveTune.Utils
{
    public class ResourceUtil
    {
        private static Geometry? GetIconForName(string name)
        {
            object? icon = null;
            Application.Current?.Resources.TryGetResource(name, Avalonia.Styling.ThemeVariant.Default, out icon);
            return (Geometry?)icon;
        }
    }
}
