namespace ToFLaC.Model.State
{
    public class TopDomainOrg : IURLFinderState
    {
        private int _countEnter = 0;

        public void Enter(URLFinder urlFinder)
        {
            if (_countEnter == 0 && urlFinder.Text[urlFinder.CurrentIdx] == 'r')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDOrg2");
                _countEnter++;
                return;
            }
            if (_countEnter == 1 && urlFinder.Text[urlFinder.CurrentIdx] == 'g')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDOrg3");
                urlFinder.TopDomain = "org";
                urlFinder.State = new Path();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
