namespace Barista.Shared.State
{
    public class InputState
    {
        public bool ExpectingUserInput { get; set; }
        public ExpectingUserInputType ExpectingUserInputType { get; set; }
    }
}
