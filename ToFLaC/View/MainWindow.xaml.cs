using System.IO;
using System.Windows;
using System.Windows.Controls;
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
        MessageBoxResult resultOfBox = MessageBox.Show("Уверены, что создать новый файл? Несохраненные данные будут удалены","Создание нового файла",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resultOfBox == MessageBoxResult.Yes)
        {
            codeBox.Clear();
            fileName = "";
        }
    }

    private void MenuDelete_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.Clear();
    }

    private void MenuSelect_OnClick(object sender, RoutedEventArgs e)
    {
        codeBox.SelectAll();
    }

    private void MenuExit_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MenuHelp_OnClick(object sender, RoutedEventArgs e)
    {
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        string fullPath = Path.Combine(projectDirectory, "View", "Help.html");


        if (File.Exists(fullPath))
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fullPath) { UseShellExecute = true });
        }
        else
        {
            MessageBox.Show($"Файл не найден: {fullPath}");
        }
    }

    private void MenuAboutProgram_OnClick(object sender, RoutedEventArgs e)
    {
        AboutProgramWindow aboutProgramWindow = new AboutProgramWindow();
        aboutProgramWindow.ShowDialog();
    }
}