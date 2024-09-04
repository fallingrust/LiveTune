using Avalonia.Controls;

namespace LiveTune.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void OnTitleBarPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as Window)?.BeginMoveDrag(e);
    }  
}
