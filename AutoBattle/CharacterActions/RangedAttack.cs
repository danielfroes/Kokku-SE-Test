using System;

namespace AutoBattle.CharacterActions
{
    public class RangedAttack : ICharacterAction
    {
        const int BASE_DAMAGE = 15;
        const int RANGE = 3;

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = character.BattleStats.CalculateDamage(BASE_DAMAGE, target.BattleStats);

            target.TakeDamage(finalDamage);

            return $"{character} attacked the {target} and did {finalDamage} damage." +
                    Environment.NewLine + $"{target} has {target.CurrentHealth} health left.";
        }
    }
}