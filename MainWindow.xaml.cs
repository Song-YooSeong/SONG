using System.Windows;
using System.Windows.Controls;

namespace CustomDialog;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // DataGrid 의 Row 에 헤더를 추가하여 순번을 부여 한다.
    private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }
    // Git Test 용 ..
}