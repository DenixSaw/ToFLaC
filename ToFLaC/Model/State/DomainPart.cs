﻿namespace ToFLaC.Model.State
{
    public class DomainPart : IURLFinderState
    {
        private string _forbiddenChars = " -!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";

        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == '.')
            {
                urlFinder.DomainCount++;
                urlFinder.State = new NextDomain();
                return;
            }
            else if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) ||
                urlFinder.CurrentIdx - urlFinder.DomainStartIdx > 63)
            {
                urlFinder.DomainCount = 0;
                urlFinder.State = new FirstEnter();
                return;
            }
        }
    }
}
