namespace ToFLaC.Model.State
{
    public struct URLPosition(
        int line, 
        int startIdx, 
        int endIdx,
        string url,
        string protocol,
        string subDomain,
        List<string> domain,
        string topDomain,
        string context
        )
    {
        public int line = line;
        public int startIdx = startIdx;
        public int endIdx = endIdx;
        public string url = url;
        public string protocol = protocol;
        public string subDomain = subDomain;
        public List<string> domain = domain;
        public string topDomain = topDomain;
        public string context = context;
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
        public int ContextStartIdx { get; set; }

        private List<URLPosition> _linesList;

        public string Protocol { get; set; }
        public string SubDomain { get; set; }
        public List<string> Domain { get; set; }
        public string TopDomain { get; set; }
        public string Context { get; set; }
        public List<string> States { get; set; } = new();
        public int cntDomain { get; set; } = 1;
        public int cntNextDomain { get; set; } = 1;
        public int cntContext { get; set; } = 1;

        public URLFinder()
        {
            _currentLine = 0;
            _linesList = new List<URLPosition>();
            State = new FirstEnter();
        }

        //public void SavePositions()
        //{
        //    _linesList.Add(new URLPosition(_currentLine, StartIdx, CurrentIdx, Text.Substring(StartIdx, CurrentIdx - StartIdx - 1)));
        //}

        public void SavePositions()
        {
            _linesList.Add(new URLPosition(_currentLine, StartIdx, CurrentIdx, Text.Substring(StartIdx, CurrentIdx - StartIdx - 1), Protocol, SubDomain, Domain, TopDomain, Context));
            ClearURLData();
        }

        public void ClearURLData()
        {
            Protocol = string.Empty;
            SubDomain = string.Empty;
            Domain = new List<string>();
            TopDomain = string.Empty;
            Context = string.Empty;
        }

        public List<URLPosition> FindUrls(string text)
        {
            List<string> strings = text.Split('\n').ToList();

            for (int strIdx = 0; strIdx < strings.Count; strIdx++)
            {
                if (State is not FirstEnter)
                {
                    State = new FirstEnter();
                }

                strings[strIdx] += " ";
                CurrentIdx = 0;
                _currentLine = strIdx + 1;

                Text = strings[strIdx];

                for (; CurrentIdx < strings[strIdx].Length;)
                {
                    State.Enter(this);
                }

                if (State is End)
                {
                    State.Enter(this);
                }
            }

            return _linesList;
        }
    }
}
