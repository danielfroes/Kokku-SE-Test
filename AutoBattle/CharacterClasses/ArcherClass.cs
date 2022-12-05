using AutoBattle.CharacterActions;
using System;
using System.Collections.Generic;

namespace AutoBattle.CharacterClasses
{
    public class ArcherClass : ICharacterClass
    {
        public string DisplaySymbol => "A";

        public string DisplayName => "Archer";

        public BattleStats BaseStats => new BattleStats(30, 5);

        public int BaseHealth => 70;

        ICharacterAction _defaultAction = new WalkAction();

        IReadOnlyList<ICharacterAction> _characterAction = new List<ICharacterAction>
        {
            new RangedAttackAction(),
            new RollbackAttack()
        };

        public IReadOnlyList<ICharacterAction> GetValidActions(int targetDistance)
        {
            List<ICharacterAction> validActions = new List<ICharacterAction>();

            foreach (ICharacterAction action in _characterAction)
            {
                //TODO: tem um problema aqui pq para ranges de 3 por exemplo,
                //uma distancia de 3 na diagonal vai da e eu nao quero q a diagonal valha
                if (action.Range >= targetDistance)
                    validActions.Add(action);

            }

            if (validActions.Count == 0)
                validActions.Add(_defaultAction);

            return validActions;
        }
    }
}
