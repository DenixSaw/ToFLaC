using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class NextDomain : IURLFinderState
    {
        private string _forbiddenChars = " ()<>[]:;@\\,\"\t\n\r";
        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'r')
            {
                urlFinder.State = new TopDomainRu();
                return;
            }
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'c')
            {
                urlFinder.State = new TopDomainCom();
                return;
            }
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'o')
            {
                urlFinder.State = new TopDomainOrg();
                return;
            }
            if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) || urlFinder.DomainCount > 3)
            {
                urlFinder.State = new FirstEnter();
                return;
            }

            urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
            urlFinder.State = new DomainPart();
            return;
        }
    }
}
