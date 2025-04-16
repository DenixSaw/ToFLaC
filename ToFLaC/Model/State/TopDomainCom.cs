﻿namespace ToFLaC.Model.State
{
    public class TopDomainCom : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?\t\n\r";
        private int _countEnter = 0;

        public void Enter(URLFinder urlFinder)
        {
            if (_countEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] == 'o')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDCom2");
                _countEnter++;
                return;
            }
            else if (_countEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] != 'o' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) &&
                urlFinder.Text[urlFinder.CurrentIdx] != '/')
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx - 1;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.cntDomain++;
                urlFinder.State = new DomainPart();
                return;
            }
            if (_countEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 'm' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                if (urlFinder.Text[urlFinder.CurrentIdx + 1] == '/')
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.States.Add("TDCom3");
                    urlFinder.TopDomain = "com";
                    urlFinder.State = new Path();
                    return;
                }
                else
                {
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx - 2;
                    urlFinder.CurrentIdx++;
                    urlFinder.States.Add($"D{urlFinder.cntDomain}");
                    urlFinder.cntDomain++;
                    urlFinder.State = new DomainPart();
                    return;
                }
            }
            else if (_countEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 'm')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDCom3");
                urlFinder.TopDomain = "com";
                urlFinder.State = new Path();
                return;
            }
            else if (_countEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] != 'm' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) &&
                urlFinder.Text[urlFinder.CurrentIdx] != '/')
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx - 2;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.cntDomain++;
                urlFinder.State = new DomainPart();
                return;
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx] == '.')
            {
                urlFinder.DomainCount++;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.Domain.Add(urlFinder.Text.Substring(urlFinder.CurrentIdx, _countEnter + 1));
                urlFinder.State = new NextDomain();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
