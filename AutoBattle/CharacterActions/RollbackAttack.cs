using System;
using System.Numerics;

namespace AutoBattle.CharacterActions
{
    public class RollbackAttack : ICharacterAction
    {
        const int BASE_DAMAGE = 10;
        const int STEPS = 2;
        const int RANGE = 1;

        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = character.BattleStats.CalculateDamage(BASE_DAMAGE, target.BattleStats);
            target.TakeDamage(finalDamage);

            Vector2 rollbackDirection = -character.Position.DirectionTo(target.Position);

            string outputMessage = $"{character} attacked the {target} dealing {finalDamage} damage";
            if (battlefield.TryMoveEntity(character, rollbackDirection, STEPS))
            {
                outputMessage += $" and rolled away to position {character.Position}";
            }
            outputMessage += Environment.NewLine + $"{target} has {target.CurrentHealth} health left.";

            return outputMessage;
        }
    }
}