using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class Context : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#^*()+{}|\\:;\"'<>,/\t\n\r";
        private int _cntChars = 0;

        public void Enter(URLFinder urlFinder)
        {
            if (!_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) && _cntChars < 30)
            {
                _cntChars++;
                if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
                {
                    urlFinder.State = new End();
                    return;
                }
                return;
            }
            else
            {
                urlFinder.State = new End();
                return;
            }
        }
    }
}
