using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public struct URLPosition(int line, int startIdx, int endIdx, string url)
    {
        public int line = line;
        public int startIdx = startIdx;
        public int endIdx = endIdx;
        public string url = url;
    }

    public class URLFinder
    {
        private int _currentLine;
        private int _startIdx;
        
        public int StartIdx
        {
            get => _startIdx;
            set => _startIdx = value;
        }

        public IURLFinderState State { get; set; }
        public string Text { get; set; }
        public int CurrentIdx { get; set; }
        public int DomainCount { get; set; }
        public int DomainStartIdx { get; set; }

        private List<URLPosition> _linesList;

        public URLFinder()
        {
            _currentLine = 0;
            _linesList = new List<URLPosition>();
            State = new FirstEnter();
        }

        public void SavePositions()
        {
            string[] lines = Text.Split('\n');
            int startLineIdx = 0;

            for (int i = 0; i < _currentLine - 1; i++)
                startLineIdx += lines[i].Length + 1;

            _linesList.Add(new URLPosition(_currentLine, StartIdx - startLineIdx, CurrentIdx - 1 - startLineIdx, Text.Substring(StartIdx, CurrentIdx - StartIdx - 1)));
        }

        public List<URLPosition> FindUrls(string text) 
        { 
            Text = text + ' ';

            for (; CurrentIdx < Text.Length; CurrentIdx++)
            {
                State.Enter(this);
                if (Text[CurrentIdx] == '\n')
                    _currentLine++;
            }

            if (State is End)
            {
                State.Enter(this);
            }

            return _linesList;
        }
    }
}
