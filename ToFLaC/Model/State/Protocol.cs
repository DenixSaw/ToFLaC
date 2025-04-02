namespace ToFLaC.Model.State
{
    public class Protocol : IURLFinderState
    {
        private string _forbiddenChars = " .+-!№#$%^&?*()<>[]{}|:;@'~`\\,\"\t\n\r";

        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'h')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PH");
                urlFinder.State = new ProtocolH();
                return;
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx] == 'f')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PF");
                urlFinder.State = new ProtocolF();
                return;
            }
            else if (!_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]))
            {
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
