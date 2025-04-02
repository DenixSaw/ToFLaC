namespace ToFLaC.Model.State
{
    public class ProtocolH : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _cntEnter = 0;
        private bool _isWithS = false;

        public string GetNameState => "P";

        public void Enter(URLFinder urlFinder)
        {
            if (
                (_cntEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] == 't') ||
                (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 't') ||
                (_cntEnter == 2 && urlFinder.Text[urlFinder.CurrentIdx] == 'p')
                )
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PH");
                _cntEnter++;
                return;
            }

            if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx] == 's')
            {
                _isWithS = true;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PHs");
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx] == ':')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PH");
                _cntEnter++;
                return;
            }

            if (_cntEnter == 4 && _isWithS && urlFinder.Text[urlFinder.CurrentIdx] == ':')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PHs");
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 4 && !_isWithS && urlFinder.Text[urlFinder.CurrentIdx] == '/')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PH");
                _cntEnter++;
                return;
            }

            if (_cntEnter == 5 && _isWithS && urlFinder.Text[urlFinder.CurrentIdx] == '/')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PHs");
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 5 && !_isWithS && urlFinder.Text[urlFinder.CurrentIdx] == '/' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                urlFinder.States.Add("PH");
                if (urlFinder.Text[urlFinder.CurrentIdx + 1] == 'w')
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.Protocol = "http";
                    urlFinder.State = new SubDomain();
                }
                else
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.Protocol = "http";
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx + 1;
                    urlFinder.State = new DomainPart();
                }
                return;
            }
            
            if (_cntEnter == 6 && _isWithS && urlFinder.Text[urlFinder.CurrentIdx] == '/' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                urlFinder.States.Add("PHs");
                if (urlFinder.Text[urlFinder.CurrentIdx + 1] == 'w')
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.Protocol = "https";
                    urlFinder.State = new SubDomain();
                }
                else
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx + 1;
                    urlFinder.Protocol = "https";
                    urlFinder.State = new DomainPart();
                }
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
