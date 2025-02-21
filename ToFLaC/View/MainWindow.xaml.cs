using System.Windows;
using ToFLaC.ViewModel;

namespace ToFLaC.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainVM();
    }

    private void ButtonCopy_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.Copy();
    }

    private void ButtonCut_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.Cut();
    }

    private void ButtonInsert_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.Paste();
    }
}