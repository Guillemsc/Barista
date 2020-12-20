using Barista.Client.Libraries;

namespace Barista.Client.View.Entities.Hero
{
    public class HeroEntityViewFactory : IHeroEntityViewFactory
    {
        private readonly HeroesLibrary heroesLibrary;

        public HeroEntityViewFactory(HeroesLibrary heroesLibrary)
        {
            this.heroesLibrary = heroesLibrary;
        }

        public HeroEntityView Create(string typeId, int instanceId)
        {
            bool found = heroesLibrary.TryGetItem(typeId, out HeroEntityView heroEntityView);

            if (!found)
            {
                throw new System.Exception($"{nameof(HeroEntityView)} of type {typeId} could not " +
                    $"be found on {nameof(HeroEntityViewFactory)}");
            }

            if (heroEntityView == null)
            {
                throw new System.Exception($"{nameof(HeroEntityView)} of type '{typeId}' and instance " +
                    $"{instanceId} was null on {nameof(HeroEntityViewFactory)}");
            }

            HeroEntityView newHeroEntityView = heroEntityView.gameObject.InstantiateAndGetComponent<HeroEntityView>();

            newHeroEntityView.Construct(typeId, instanceId);

            return newHeroEntityView;
        }

        public void Destroy(HeroEntityView toDestroy)
        {
            toDestroy.CleanUp();

            toDestroy.DestroyGameObject();
        }
    }
}