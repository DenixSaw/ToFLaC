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
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("C");
                _cntChars++;
                if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.Context = urlFinder.Text.Substring(urlFinder.ContextStartIdx, urlFinder.CurrentIdx - urlFinder.ContextStartIdx);
                    urlFinder.State = new End();
                    return;
                }
                return;
            }
            else
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("C");
                urlFinder.Context = urlFinder.Text.Substring(urlFinder.ContextStartIdx, urlFinder.CurrentIdx - urlFinder.ContextStartIdx);
                urlFinder.State = new End();
                return;
            }
        }
    }
}
