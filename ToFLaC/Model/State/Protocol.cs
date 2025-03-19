namespace ToFLaC.Model.State
{
    public class Protocol : IURLFinderState
    {
        private string _forbiddenChars = " .+-!№#$%^&?*()<>[]{}|:;@'~`\\,\"\t\n\r";

        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx - 1] == 'h')
            {
                urlFinder.State = new ProtocolH();
                return;
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx - 1] == 'f')
            {
                urlFinder.State = new ProtocolF();
                return;
            }
            else if (!_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
                urlFinder.CurrentIdx--;
                urlFinder.State = new SubDomain();
                return;
            }
            else
            {
                urlFinder.State = new FirstEnter();
                return;
            }

        }
    }
}
