using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class End : IURLFinderState
    {
        public void Enter(URLFinder urlFinder)
        {
            urlFinder.SavePositions();
            urlFinder.State = new FirstEnter();
        }
    }
}
