using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class ProtocolF : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _cntEnter = 0;

        public void Enter(URLFinder urlFinder)
        {
            if (
                (_cntEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 't') || 
                (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 'p') || 
                (_cntEnter == 2 && urlFinder.Text[urlFinder.CurrentIdx - 1] == ':') ||
                (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx - 1] == '/')
                )
            {
                _cntEnter++;
                return;
            }
            if (_cntEnter == 4 && urlFinder.Text[urlFinder.CurrentIdx - 1] == '/')
            {
                if (urlFinder.Text[urlFinder.CurrentIdx] == 'w')
                {
                    urlFinder.State = new SubDomain();
                    return;
                }
                else if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
                {
                    urlFinder.State = new FirstEnter();
                    return;
                }
                else
                {
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
                    urlFinder.State = new DomainPart();
                    return;
                }
            }
            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
