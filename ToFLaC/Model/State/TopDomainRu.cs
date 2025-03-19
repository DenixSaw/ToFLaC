using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class TopDomainRu : IURLFinderState
    {
        //private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";

        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'u')
            {
                urlFinder.State = new Path();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
