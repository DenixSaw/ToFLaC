using ToFLaC.ViewModel.Commands;
using System.Windows.Controls;
using ToFLaC.Model.State;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Shapes;
using ToFLaC.Model.Tokens;
using ToFLaC.Model.RecDescent;

namespace ToFLaC.ViewModel;

public class MainVM : BaseVM
{
    private string enteredCode = string.Empty;
    private string indexesNumbers = "1\n";
    private int numbersCount = 1;
    

    private string _outputText = string.Empty;

    public string EnteredCode
    {
        get => enteredCode;
        set
        {
            Set(ref enteredCode, value);
            
            UpdateIndexesNumbers(value);
        }
    }
    
    public string OutputText
    {
        get => _outputText;
        set => Set(ref _outputText, value);
    }

    public string IndexesNumbers
    {
        get => indexesNumbers;
        set => Set(ref indexesNumbers, value);
    }
    
    private void UpdateIndexesNumbers(string value)
    {
        int numbersSplits = value.Split('\n').Length;
        if (numbersCount < numbersSplits)
        {
            numbersCount = numbersSplits;
            IndexesNumbers = IndexesNumbers.Split('\n')[0] + "\n";
            for (int i = 2; i < numbersSplits + 1; i++)
            {
                IndexesNumbers += $"{i}\n";
            }
        }

        if (numbersCount > numbersSplits)
        {
            numbersCount -= 1;
            IndexesNumbers = IndexesNumbers.Split('\n')[0] + "\n";
            for (int i = 2; i < numbersSplits + 1; i++)
            {
                IndexesNumbers += $"{i}\n";
            }
        }
    }
    
    public Command StartCommand => Command.Create(Start);
    public Command ClearAllCommand => Command.Create(ClearAll);

    public void ClearAll()
    {
        EnteredCode = string.Empty;
        OutputText = string.Empty;
    }

    private void Start()
    {
        //URLFinder _urlFinder = new();
        //List<URLPosition> _urls = new();
        //string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        //string fullPath = System.IO.Path.Combine(projectDirectory, "Files", "urls.txt");
        //FileStream fileStream = File.Create(fullPath);
        //fileStream?.Close();
        //string formattedText = EnteredCode.Replace("\r", "");
        //_urls = _urlFinder.FindUrls(formattedText);
        //using (StreamWriter writer = new StreamWriter(fullPath, false))
        //{
        //    foreach (URLPosition url in _urls)
        //    {
        //        string domains = string.Empty;
        //        foreach (string domain in url.domain)
        //            domains += domain + " ";

        //        OutputText += $"Найдена ссылка: {url.url}\nСтрока: {url.line}\nИндекс начала ссылки: {url.startIdx}\nИндекс конца ссылки: {url.endIdx}\nПротокол: {url.protocol}\nПоддомен: {url.subDomain}\nДомены: {domains}\nДомен верхнего уровня: {url.topDomain}\nКонтекст: {url.context}\n\n";

        //        writer.WriteLine($"Найдена ссылка: {url.url}\nСтрока: {url.line}\nИндекс начала ссылки: {url.startIdx}\nИндекс конца ссылки: {url.endIdx}\nПротокол: {url.protocol}\nПоддомен: {url.subDomain}\nДомены: {domains}\nДомен верхнего уровня: {url.topDomain}\nКонтекст: {url.context}\n\n");
        //    }

        //    OutputText += "История переходов:\n";
        //    for (int i = 0; i < _urlFinder.States.Count; i++)
        //    {
        //        OutputText += _urlFinder.States[i];
        //        if (i != _urlFinder.States.Count - 1)
        //            OutputText += " -> ";
        //    }
        //    OutputText += "\n\n";
        //}

        List<Token> tokens = TokenBuilder.FindTokens(EnteredCode);
        RecursiveDescent parser = new(tokens);

        parser.Start();
        OutputText += "Последовательность разбора:\n";
        for (int i = 0; i < parser.ParseSequence.Count; i++)
        {
            if (i == parser.ParseSequence.Count - 1)
                OutputText += parser.ParseSequence[i];
            else
                OutputText += parser.ParseSequence[i] + "__";
        }
        OutputText += "\n";
    }
}