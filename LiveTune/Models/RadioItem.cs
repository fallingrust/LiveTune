using LiveTune.ViewModels;

namespace LiveTune.Models
{
    public class RadioItem : ViewModelBase
    {
        private string _content = string.Empty;
        private bool _isSelected = false;

        public string Content { get => _content; set => SetProperty(ref _content, value); }
        public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }

        public RadioItem(string content, bool isSelected = false)
        {
            Content = content;
            IsSelected = isSelected;
        }
    }
}
