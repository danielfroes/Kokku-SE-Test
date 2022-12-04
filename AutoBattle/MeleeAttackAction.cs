
namespace AutoBattle
{
    public class MeleeAttackAction : ICharacterAction
    {
        public int Range => 1;

        int _baseDamage = 20;
        
        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = BattleStats.CalculateDamage(_baseDamage, character, target);

            target.TakeDamage(finalDamage);

            return $"Player {character.DisplaySymbol} is attacking the player {target.DisplaySymbol} and did {finalDamage} damage\n";
        }
    }
}