using Barista.Shared.Entities.Enemy;
using Barista.Shared.Logic.EnemyActions;
using Juce.Core.State;
using System.Collections.Generic;

namespace Barista.Shared.Logic.States
{
    public class EnemiesTurnState : IStateMachineStateAction
    {
        private readonly LevelLogic levelLogic;

        public EnemiesTurnState(LevelLogic levelLogic)
        {
            this.levelLogic = levelLogic;
        }

        public void OnEnter()
        {
            List<EnemyEntity> enemyEntities = levelLogic.UseCases.GetEnemiesToPerformTurnUseCase.Get();

            foreach(EnemyEntity enemyEntity in enemyEntities)
            {
                EnemyActionType enemyActionType = levelLogic.UseCases.GetEnemyNextActionUseCase.Get(enemyEntity);

                levelLogic.UseCases.PerformEnemyActionUseCase.Perform(enemyEntity, enemyActionType);
            }

            levelLogic.StateMachine.Next(LevelLogicState.TurnEnd);
        }

        public void OnExit()
        {

        }
    }
}
