using System;
using System.Text;

namespace AutoBattle.CharacterActions
{
    public class CurseAction : ICharacterAction
    {
        const int ADDED_DEBUFF_STAGE = -1;

        const int RANGE = 10;

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            target.BattleStats.AddDefenseStage(ADDED_DEBUFF_STAGE);

            return $"{character} cursed {target}, lowering its defense stat.";
        }
    }
}