using System;

namespace AutoBattle.CharacterActions
{
    public class MeleeAttack : ICharacterAction
    {
        const int BASE_DAMAGE = 20;
        const int RANGE = 1;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = character.BattleStats.CalculateDamage(BASE_DAMAGE, target.BattleStats);

            target.TakeDamage(finalDamage);

            return $"{character} attacked the {target} and did {finalDamage} damage." +
                    Environment.NewLine + $"{target} has {target.CurrentHealth} health left." +
                    Environment.NewLine;
        }

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }
    }
}