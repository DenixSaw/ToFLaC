namespace ToFLaC.Model.State
{
    public class SubDomain : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _cntEnter = 0;
        private bool _isWithOne = false;

        public void Enter(URLFinder urlFinder)
        {
            if (
                (_cntEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] == 'w') ||
                (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 'w') ||
                (_cntEnter == 2 && urlFinder.Text[urlFinder.CurrentIdx] == 'w')
                )
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"SD{_cntEnter + 1}");
                _cntEnter++;
                return;
            }
            else if(urlFinder.Text[urlFinder.CurrentIdx] == 'w' && _cntEnter == 3 && !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx + 1;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.cntDomain++;
                urlFinder.State = new DomainPart();
                return;
            }

            if (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == '1')
            {
                urlFinder.DomainStartIdx = (urlFinder.CurrentIdx - _cntEnter);
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.cntDomain++;
                urlFinder.CurrentIdx++;
                urlFinder.State = new DomainPart();
                return;
            }

            if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx] == '1')
            {
                _isWithOne = true;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("SD1_1");
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx] == '.' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx + 1;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("S");
                urlFinder.SubDomain = "www";
                urlFinder.State = new DomainPart();
                return;
            }

            if (_cntEnter == 4 && _isWithOne && urlFinder.Text[urlFinder.CurrentIdx] == '.' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx + 1;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("S");
                urlFinder.SubDomain = "www1";
                urlFinder.State = new DomainPart();
                return;
            }

            if (!_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                urlFinder.DomainStartIdx = (urlFinder.CurrentIdx - _cntEnter);
                urlFinder.State = new DomainPart();
                return;
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx] == '.')
            {
                urlFinder.DomainStartIdx = (urlFinder.CurrentIdx - _cntEnter);
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.cntDomain++;
                urlFinder.CurrentIdx++;
                urlFinder.DomainCount++;
                urlFinder.Domain.Add(urlFinder.Text.Substring(urlFinder.DomainStartIdx, urlFinder.CurrentIdx - urlFinder.DomainStartIdx - 1));
                urlFinder.State = new NextDomain();
                return;
            }
            else
            {
                urlFinder.State = new FirstEnter();
                return;
            }
        }
    }
}
