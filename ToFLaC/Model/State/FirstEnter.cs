namespace ToFLaC.Model.State
{
    public class FirstEnter : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/`~";

        public void Enter(URLFinder urlFinder)
        {
            urlFinder.ClearURLData();
            urlFinder.DomainCount = 0;
            if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx])) 
            {
                urlFinder.CurrentIdx++;
                return;
            }
            urlFinder.StartIdx = urlFinder.CurrentIdx;
            urlFinder.States.Add("FE");
            urlFinder.State = new Protocol();
        }
    }
}
