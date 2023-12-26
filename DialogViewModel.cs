using System.ComponentModel;
using System.Windows;

namespace CustomDialog;

public class DialogViewModel : INotifyPropertyChanged
{
 
    private string? _contents;

    public string Contents
    {
        get
        {
            return _contents;
        }

        set
        {
            if (_contents != value)
            {
                _contents = value;
                OnPropertyChanged(nameof(Contents));
            }
        }
    }

    public DialogViewModel()
    {
    }

    public DialogViewModel(string contents)
    {
        Contents = contents;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}