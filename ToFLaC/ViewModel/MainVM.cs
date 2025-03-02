namespace ToFLaC.ViewModel;

public class MainVM : BaseVM
{
    string enteredCode = string.Empty;
    string indexesNumbers = "0\n";
    int numbersCount = 0;

    public string EnteredCode
    {
        get => enteredCode;
        set
        {
            Set(ref enteredCode, value);
            int numbersSplits = value.Split('\n').Length - 1;
            if (numbersCount < numbersSplits)
            {
                numbersCount += 1;
                IndexesNumbers += $"{value.Split('\n').Length - 1}\n";
            }

            if (numbersCount > numbersSplits)
            {
                numbersCount -= 1;
                IndexesNumbers = IndexesNumbers.Split('\n')[0]+"\n";
                for (int i = 1; i < numbersSplits + 1; i++)
                {
                    IndexesNumbers += $"{i}\n";
                }
            }
        }
    }

    public string IndexesNumbers
    {
        get => indexesNumbers;
        set => Set(ref indexesNumbers, value);
    }
}