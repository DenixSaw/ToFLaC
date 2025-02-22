namespace ToFLaC.ViewModel;

public class MainVM : BaseVM
{
    string enteredCode = string.Empty;

    public string EnteredCode
    {
        get => enteredCode;
        set => Set(ref enteredCode, value);
    }
}