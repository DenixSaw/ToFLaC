namespace ToFLaC.Model.State
{
    public class ProtocolH : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _cntEnter = 0;
        private bool _isWithS = false;

        public void Enter(URLFinder urlFinder)
        {
            if (
                (_cntEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 't') ||
                (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 't') ||
                (_cntEnter == 2 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 'p')
                )
            {
                _cntEnter++;
                return;
            }

            if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 's')
            {
                _isWithS = true;
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx - 1] == ':')
            {
                _cntEnter++;
                return;
            }

            if (_cntEnter == 4 && _isWithS && urlFinder.Text[urlFinder.CurrentIdx - 1] == ':')
            {
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 4 && !_isWithS && urlFinder.Text[urlFinder.CurrentIdx - 1] == '/')
            {
                _cntEnter++;
                return;
            }

            if (_cntEnter == 5 && _isWithS && urlFinder.Text[urlFinder.CurrentIdx - 1] == '/')
            {
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 5 && !_isWithS && urlFinder.Text[urlFinder.CurrentIdx - 1] == '/' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                if (urlFinder.Text[urlFinder.CurrentIdx] == 'w')
                {
                    urlFinder.CurrentIdx--;
                    urlFinder.State = new SubDomain();
                }
                else
                {
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
                    urlFinder.State = new DomainPart();
                }
                return;
            }
            
            if (_cntEnter == 6 && _isWithS && urlFinder.Text[urlFinder.CurrentIdx - 1] == '/' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                if (urlFinder.Text[urlFinder.CurrentIdx] == 'w')
                {
                    urlFinder.State = new SubDomain();
                }
                else
                {
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
                    urlFinder.State = new DomainPart();
                }
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
