using System.Windows;

namespace CustomDialog;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class CustomDialogWindow : Window
{
    public CustomDialogWindow()
    {
        InitializeComponent();
    }

   public CustomDialogWindow(DialogViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }

}