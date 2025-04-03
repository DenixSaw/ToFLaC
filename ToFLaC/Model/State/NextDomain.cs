namespace ToFLaC.Model.State
{
    public class NextDomain : IURLFinderState
    {
        private string _forbiddenChars = " ()<>[]:;@\\,\"\t\n\r";

        public string GetNameState => "";

        public void Enter(URLFinder urlFinder)
        {
            urlFinder.States.Add("ND");
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'r')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDRu1");
                urlFinder.State = new TopDomainRu();
                return;
            }
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'c')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDCom1");
                urlFinder.State = new TopDomainCom();
                return;
            }
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'o')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDOrg1");
                urlFinder.State = new TopDomainOrg();
                return;
            }
            if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) || urlFinder.DomainCount > 3)
            {
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
