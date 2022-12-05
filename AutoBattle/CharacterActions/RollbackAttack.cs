using System.Numerics;

namespace AutoBattle.CharacterActions
{
    public class RollbackAttack : ICharacterAction
    {
        const int BASE_DAMAGE = 10;
        const int STEPS = 2;
        public int Range => 1;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = BattleStats.CalculateDamage(BASE_DAMAGE, character, target);
            target.TakeDamage(finalDamage);
            
            string outputMessage = $"Character {character.DisplaySymbol} is attacking " +
                   $"the character {target.DisplaySymbol} and did {finalDamage} damage\n." +
                   $"Character {target.DisplaySymbol} has {target.CurrentHealth}";

            Vector2 rollbackDirection = - character.Position.DirectionTo(target.Position);

            if (battlefield.TryMoveEntity(character, rollbackDirection, STEPS))
            {
                outputMessage += $"\n Character {character.DisplaySymbol} rolled to Position {character.Position}";
            }

            return outputMessage;
        }
    }
}