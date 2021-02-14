namespace Barista.Shared.Events
{
    public class ExpectingHeroActionChangedOutEvent
    {
        public bool Expecting { get; }

        public ExpectingHeroActionChangedOutEvent(bool expecting)
        {
            Expecting = expecting;
        }
    }
}