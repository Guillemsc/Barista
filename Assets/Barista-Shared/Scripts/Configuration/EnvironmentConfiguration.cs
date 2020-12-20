namespace Barista.Shared.Configuration
{
    public class EnvironmentConfiguration
    {
        public string TypeId { get; }

        public EnvironmentConfiguration(string typeId)
        {
            TypeId = typeId;
        }
    }
}