using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                (_cntEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 'w') ||
                (_cntEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 'w') ||
                (_cntEnter == 2 && urlFinder.Text[urlFinder.CurrentIdx - 1] == 'w')
                )
            {
                _cntEnter++;
                return;
            }
            else if(urlFinder.Text[urlFinder.CurrentIdx - 1] != 'w' && !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx - 1]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
                urlFinder.State = new DomainPart();
                return;
            }

            if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx - 1] == '1')
            {
                _isWithOne = true;
                _cntEnter++;
                return;
            }
            else if (_cntEnter == 3 && urlFinder.Text[urlFinder.CurrentIdx - 1] == '.' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
                urlFinder.State = new DomainPart();
                return;
            }

            if (_cntEnter == 4 && _isWithOne && urlFinder.Text[urlFinder.CurrentIdx - 1] == '.' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
                urlFinder.State = new DomainPart();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
