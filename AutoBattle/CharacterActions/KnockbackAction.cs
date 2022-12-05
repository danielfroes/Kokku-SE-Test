using AutoBattle.CharacterActions;
using System.Numerics;
using System;

namespace AutoBattle.CharacterClasses
{
    public class KnockbackAction : ICharacterAction
    {
        const int BASE_DAMAGE = 10;
        const int STEPS = 2 ;
        const int RANGE = 2;

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = character.BattleStats.CalculateDamage(BASE_DAMAGE, target.BattleStats);
            target.TakeDamage(finalDamage);

            string outputMessage = $"{character} emmited a wave of energy in the {target}, dealing {finalDamage} damage";

            Vector2 knockbackDirection = character.Position.DirectionTo(target.Position);
            if (battlefield.TryMoveEntity(target, knockbackDirection, STEPS))
            {
                outputMessage += $" and knocking it back to the position {target.Position}";
            }

            outputMessage += Environment.NewLine + $"{target} has {target.CurrentHealth} health left.";

            return outputMessage;
        }
    }
}