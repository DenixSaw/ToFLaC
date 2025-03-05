using ToFLaC.ViewModel.Commands;
using System.Windows.Controls;

namespace ToFLaC.ViewModel;

public class MainVM : BaseVM
{
    private string enteredCode = string.Empty;
    private string indexesNumbers = "0\n";
    private int numbersCount = 0;

    private Stack<(string Text, int CaretPosition)> _undoStack = new();
    private Stack<(string Text, int CaretPosition)> _redoStack = new();
    private bool _isUndoRedoOperation = false;
    private TextBox? _textBox;

    public string EnteredCode
    {
        get => enteredCode;
        set
        {
            if (!_isUndoRedoOperation)
            {
                _undoStack.Push((enteredCode, _textBox?.CaretIndex ?? 0));
                _redoStack.Clear();
            }

            Set(ref enteredCode, value);

            UpdateIndexesNumbers(value);
        }
    }

    public string IndexesNumbers
    {
        get => indexesNumbers;
        set => Set(ref indexesNumbers, value);
    }

    public Command UndoCommand => Command.Create(Undo);
    public Command RedoCommand => Command.Create(Redo);

    public void AttachTextBox(TextBox textBox)
    {
        _textBox = textBox;
        _undoStack = new();
        _redoStack = new();
    }

    private void UpdateIndexesNumbers(string value)
    {
        int numbersSplits = value.Split('\n').Length - 1;
        if (numbersCount < numbersSplits)
        {
            numbersCount = numbersSplits;
            IndexesNumbers = IndexesNumbers.Split('\n')[0] + "\n";
            for (int i = 1; i < numbersSplits + 1; i++)
            {
                IndexesNumbers += $"{i}\n";
            }
        }

        if (numbersCount > numbersSplits)
        {
            numbersCount -= 1;
            IndexesNumbers = IndexesNumbers.Split('\n')[0] + "\n";
            for (int i = 1; i < numbersSplits + 1; i++)
            {
                IndexesNumbers += $"{i}\n";
            }
        }
    }

    private void Undo()
    {
        if (_undoStack.Count > 0)
        {
            _isUndoRedoOperation = true;

            _redoStack.Push((enteredCode, _textBox?.CaretIndex ?? 0));

            var (previousText, previousCaretPosition) = _undoStack.Pop();
            EnteredCode = previousText;

            _isUndoRedoOperation = false;

            if (_textBox != null)
            {
                _textBox.CaretIndex = Math.Min(previousCaretPosition, previousText.Length);
            }
        }
    }

    private void Redo()
    {
        if (_redoStack.Count > 0)
        {
            _isUndoRedoOperation = true;
            _undoStack.Push((enteredCode, _textBox?.CaretIndex ?? 0));

            var (redoText, redoCaretPosition) = _redoStack.Pop();
            EnteredCode = redoText;

            _isUndoRedoOperation = false;
            if (_textBox != null)
            {
                _textBox.CaretIndex = Math.Min(redoCaretPosition, redoText.Length);
            }
        }
    }
}