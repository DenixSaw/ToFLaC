namespace ToFLaC.Model.State
{
    public class TopDomainCom : IURLFinderState
    {
        private int _countEnter = 0;

        public void Enter(URLFinder urlFinder)
        {
            if (_countEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] == 'o')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDCom2");
                _countEnter++;
                return;
            }
            if (_countEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 'm')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDCom3");
                urlFinder.TopDomain = "com";
                urlFinder.State = new Path();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
