using System;

namespace AutoBattle.CharacterActions
{
    public class ShieldAttack : ICharacterAction
    {
        const int BASE_DAMAGE = 10;
        const int DEFENSE_STAGES_ADDED = 1;
        const int RANGE = 1;

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            character.BattleStats.AddDefenseStage(DEFENSE_STAGES_ADDED);

            int finalDamage = character.BattleStats.CalculateDamage(BASE_DAMAGE, target.BattleStats);
            target.TakeDamage(finalDamage);

            return $"{character} attacked with its shield boosting its defense and dealing {finalDamage} damage to {target}" +
                    $"{Environment.NewLine}{target} has {target.CurrentHealth} health left.";
        }
    }
}