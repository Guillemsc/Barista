namespace Barista.Shared.Logic
{
    public enum LevelLogicState
    {
        Setup,
        Start,
        WaitingForHeroAction,
        WaitingForHeroActionTarget,
        TurnStart,
        HeroTurn,
        EnemiesTurn,
        TurnEnd,
    }
}
