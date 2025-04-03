using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class TopDomainRu : IURLFinderState
    {
        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'u')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDRu2");
                urlFinder.TopDomain = "ru";
                urlFinder.State = new Path();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
