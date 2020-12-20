namespace Barista.Client.State.Persistance
{
    public interface IPersistantEntity
    {
        void Save();

        void Load();
    }
}