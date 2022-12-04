using System.Collections.Generic;

namespace AutoBattle
{
    public class PaladinClass : ICharacterClass
    {
        IReadOnlyList<ICharacterAction> _characterAction = new List<ICharacterAction>
        {
            new MeleeAttackAction(),
        };

        ICharacterAction _defaultAction = new WalkAction();

        public BattleStats BaseStats => new BattleStats(20, 20);
        public int BaseHealth => 100;

        public string DisplaySymbol => "P";
        public string DisplayName => "Paladin";

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