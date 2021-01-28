namespace Barista.Shared.Logic.Items
{
    public interface IItem
    {
        ItemType Type { get; }
        int Stacks { get; }

        void AddStack();
        void RemoveStack();
    }
}
