using System.IO;
using System.Windows;
using ToFLaC.ViewModel;
using Microsoft.Win32;

namespace ToFLaC.View;

public partial class MainWindow : Window
{
    private SaveFileDialog saveFileDialog;
    private OpenFileDialog openFileDialog;
    private string fileName = string.Empty;
    
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainVM();
        
        saveFileDialog = new SaveFileDialog();
        saveFileDialog.DefaultExt = ".txt";
        saveFileDialog.Filter = "Text documents (.txt)|*.txt";
        
        openFileDialog = new OpenFileDialog();
        openFileDialog.DefaultExt = ".txt";
        openFileDialog.Filter = "Text documents (.txt)|*.txt";
        
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

    private void ButtonSaveAs_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult resultOfBox = MessageBox.Show("Уверены, что хотите сохранить файл?","Сохранение",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resultOfBox == MessageBoxResult.Yes)
        {
            if (fileName != string.Empty)
            {
                saveFileDialog.FileName = fileName;
            }
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(saveFileDialog.FileName, codeBox.Text);
            }
        }
    }
    
    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(fileName);
        MessageBoxResult resultOfBox = MessageBox.Show("Уверены, что хотите сохранить файл?","Сохранение",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resultOfBox == MessageBoxResult.Yes)
        {
            if (fileName != string.Empty || fileName != "")
            {
                saveFileDialog.FileName = fileName;
                File.WriteAllText(saveFileDialog.FileName, codeBox.Text);
            } else if (fileName == "")
            {
                bool? result = saveFileDialog.ShowDialog();
                if (result == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, codeBox.Text);
                    fileName = saveFileDialog.FileName;
                }
            }
        }
    }

    private void ButtonOpen_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult resultOfBox = MessageBox.Show("Уверены, что хотите открыть файл? Несохраненные данные будут удалены","Открытие файла",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resultOfBox == MessageBoxResult.Yes)
        {
            bool? result = openFileDialog.ShowDialog();
            fileName = openFileDialog.FileName;
            if (result == true)
            {
                codeBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }

    private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.Clear();
        fileName = "";
    }

    private void MenuDelete_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.Clear();
    }

    private void MenuSelect_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.SelectAll();
    }
}