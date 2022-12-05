using System.Collections.Generic;
using AutoBattle.CharacterActions;
using AutoBattle.CharacterClasses;

namespace AutoBattle
{
    public class Character : IBattlefieldEntity, IBattleStatsHolder
    {
        public Character Target { get; set; }
        public Position Position { get; set; }
        public string DisplaySymbol { get; }
        public BattleStats BattleStats { get; }
        public bool IsDead {get; private set;}
        public int CurrentHealth { get; private set;}

        ICharacterClass _characterClass;


        public Character(ICharacterClass characterClass, string symbolSuffix)
        {
            _characterClass = characterClass;
            DisplaySymbol = characterClass.DisplaySymbol + symbolSuffix;
            CurrentHealth = characterClass.BaseHealth;
            BattleStats = characterClass.BaseStats;
        }


        public ICharacterAction DecideAction(Character target)
        {
            int targetDistance = Position.Distance(target.Position);

            IReadOnlyList<ICharacterAction> validActions = _characterClass.GetValidActions(targetDistance);

            return validActions.GetRandomElement();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            Position.Vacate();
            IsDead = true;
        }

        
    }
}

// -> Fiz uma abtração das classes com a interface CharacterClass
// -> Movimento virou uma ação do character, assim como ataque e ataques especiais
// -> Tirei o playerIndex e transformei em um DisplaySymbol para diferenciar eles.
