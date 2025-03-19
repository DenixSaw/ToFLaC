using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class Path : IURLFinderState
    {
        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == '/')
            {
                urlFinder.State = new Context();
                return;
            }

            urlFinder.State = new End();
            return;
        }
    }
}
