namespace AutoBattle
{
    public class Types
    {

        public struct CharacterClassSpecific
        {
            ICharacterClass CharacterClass;
            float hpModifier;
            float ClassDamage;
            CharacterSkills[] skills;

        }

        

        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

       
    }
}
