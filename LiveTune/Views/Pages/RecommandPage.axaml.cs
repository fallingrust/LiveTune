using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.Models;

namespace LiveTune.Views.Pages;

public partial class RecommandPage : UserControl
{
    public RecommandPage()
    {
        InitializeComponent();
    }

    private void OnTopClickButtonClick(object? sender, RoutedEventArgs e)
    {
        var page = new RecommandDetailPage(RecommandType.TopClick);
        WeakReferenceMessenger.Default.Send(new Messages.NavigateViewMessage(page));
    }
    private void OnTopVoteButtonClick(object? sender, RoutedEventArgs e)
    {
        var page = new RecommandDetailPage(RecommandType.TopVote);
        WeakReferenceMessenger.Default.Send(new Messages.NavigateViewMessage(page));
    }
    private void OnRencentClickButtonClick(object? sender, RoutedEventArgs e)
    {
        var page = new RecommandDetailPage(RecommandType.RencentClick);
        WeakReferenceMessenger.Default.Send(new Messages.NavigateViewMessage(page));
    }
}