using System;

namespace AutoBattle.CharacterActions
{
    public class HealAction : ICharacterAction
    {
        const int HEAL_AMOUNT = 15;

        public bool IsInRange(int targetDistance) => true;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            character.Heal(HEAL_AMOUNT);

            return $"{character} Receveid a blessing from heaven and it healead itself {HEAL_AMOUNT};" +
                    Environment.NewLine + $"{character} has {character.CurrentHealth} health left." +
                    Environment.NewLine;
        }
    }
}