using System;

namespace AutoBattle.CharacterActions
{
    public class ScareAction : ICharacterAction
    {
        const int ADDED_DEBUFF_STAGE = -1;

        const int RANGE = 10;

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            target.BattleStats.AddAttackStage(ADDED_DEBUFF_STAGE);

            return $"{character} scared {target}, lowering its attack stat.";
        }
    }
}