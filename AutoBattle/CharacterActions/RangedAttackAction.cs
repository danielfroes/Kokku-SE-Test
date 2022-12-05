namespace AutoBattle.CharacterActions
{
    public class RangedAttackAction : ICharacterAction
    {
        const int BASE_DAMAGE = 15;
        public int Range => 3;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = BattleStats.CalculateDamage(BASE_DAMAGE, character, target);
            
            target.TakeDamage(finalDamage);

            return $"Character {character.DisplaySymbol} is attacking " +
                   $"the character {target.DisplaySymbol} and did {finalDamage} damage\n." +
                   $"Character {target.DisplaySymbol} has {target.CurrentHealth}";
        }
    }
}