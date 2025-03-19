namespace ToFLaC.Model.State
{
    public class FirstEnter : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/`~";
        public void Enter(URLFinder urlFinder)
        {
            if (_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx])) return;
            urlFinder.StartIdx = urlFinder.CurrentIdx;
            urlFinder.State = new Protocol();
        }
    }
}
