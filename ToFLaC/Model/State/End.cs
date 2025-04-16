namespace ToFLaC.Model.State
{
    public class End : IURLFinderState
    {
        public void Enter(URLFinder urlFinder)
        {
            urlFinder.States.Add("END");
            urlFinder.SavePositions();
            urlFinder.State = new FirstEnter();
        }
    }
}
