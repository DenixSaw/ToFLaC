namespace ToFLaC.Model.State
{
    public class Path : IURLFinderState
    {
        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == '/')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("PATH");
                urlFinder.ContextStartIdx = urlFinder.CurrentIdx;
                urlFinder.State = new Context();
                return;
            }

            urlFinder.CurrentIdx++;
            urlFinder.States.Add("PATH");
            urlFinder.State = new End();
            return;
        }
    }
}
