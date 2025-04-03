namespace ToFLaC.Model.State
{
    public class ProtocolF : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _cntEnter = 0;

        public void Enter(URLFinder urlFinder)
        {
            if (
                (_cntEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] == 't') || 
                (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 'p') || 
                (_cntEnter == 2 && urlFinder.Text[urlFinder.CurrentIdx] == ':') ||
                (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx] == '/')
                )
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"PF{_cntEnter + 2}");
                _cntEnter++;
                return;
            }
            if (_cntEnter == 4 && urlFinder.Text[urlFinder.CurrentIdx] == '/')
            {
                if (urlFinder.Text[urlFinder.CurrentIdx + 1] == 'w')
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.States.Add("PF6");
                    urlFinder.Protocol = "ftp";
                    urlFinder.State = new SubDomain();
                    return;
                }
                else if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
                {
                    urlFinder.State = new FirstEnter();
                    return;
                }
                else
                {
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx + 1;
                    urlFinder.CurrentIdx++;
                    urlFinder.States.Add("PF6");
                    urlFinder.Protocol = "ftp";
                    urlFinder.State = new DomainPart();
                    return;
                }
            }
            else if (!_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx - _cntEnter - 1;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PF6");
                urlFinder.State = new DomainPart();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
