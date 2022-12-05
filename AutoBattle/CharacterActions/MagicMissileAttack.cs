using System;

namespace AutoBattle.CharacterActions
{
    public class MagicMissileAttack : ICharacterAction
    {
        const int BASE_DAMAGE = 10;
        public bool IsInRange(int targetDistance) => true;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = character.BattleStats.CalculateDamage(BASE_DAMAGE, target.BattleStats);

            target.TakeDamage(finalDamage);

            return $"{character} launched magic missiles in the {target} and did {finalDamage} damage." +
                    Environment.NewLine + $"{target} has {target.CurrentHealth} health left.";
        }
    }
}