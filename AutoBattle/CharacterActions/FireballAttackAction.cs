namespace AutoBattle.CharacterActions
{
    public class FireballAttackAction : ICharacterAction
    {
        const int BASE_DAMAGE = 10;
        public int Range => 2;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            int finalDamage = BattleStats.CalculateDamage(BASE_DAMAGE, character, target);

            target.TakeDamage(finalDamage);

           // target.OnEndTurn += DealBurnDamage(target);

            return $"Character {character.DisplaySymbol} is attacking " +
                   $"the character {target.DisplaySymbol} and did {finalDamage} damage\n." +
                   $"Character {target.DisplaySymbol} has {target.CurrentHealth}";
        }

        public void DealBurnDamage()
        { 
        }
    }
}