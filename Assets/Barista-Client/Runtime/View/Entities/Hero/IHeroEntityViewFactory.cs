using System;

namespace Barista.Client.View.Entities.Hero
{
    public interface IHeroEntityViewFactory
    {
        HeroEntityView Create(string typeId, int instanceId);

        void Destroy(HeroEntityView toDestroy);
    }
}