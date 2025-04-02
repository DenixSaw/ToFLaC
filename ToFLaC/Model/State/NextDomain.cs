namespace ToFLaC.Model.State
{
    public class NextDomain : IURLFinderState
    {
        private string _forbiddenChars = " ()<>[]:;@\\,\"\t\n\r";

        public string GetNameState => "";

        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'r')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDRu");
                urlFinder.State = new TopDomainRu();
                return;
            }
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'c')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDCom");
                urlFinder.State = new TopDomainCom();
                return;
            }
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'o')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDOrg");
                urlFinder.State = new TopDomainOrg();
                return;
            }
            if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) || urlFinder.DomainCount > 3)
            {
                urlFinder.States.Add("ERR");
                urlFinder.State = new FirstEnter();
                return;
            }

            urlFinder.DomainStartIdx = urlFinder.CurrentIdx;
            urlFinder.CurrentIdx++;
            urlFinder.States.Add("D");
            urlFinder.State = new DomainPart();
            return;
        }
    }
}
