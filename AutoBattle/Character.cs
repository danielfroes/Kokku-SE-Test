using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AutoBattle
{
    public class Character : IBattlefieldEntity, IBattleStatsHolder
    {
        public Character Target { get; set; }
        public GridCell Position { get; set; }
        public string DisplaySymbol { get; }
        public BattleStats BattleStats { get; }
        public bool IsDead {get; private set;}

        ICharacterClass _characterClass;

        int _currentHealth;

        public Character(ICharacterClass characterClass, string symbolSuffix)
        {
            _characterClass = characterClass;
            DisplaySymbol = characterClass.DisplaySymbol + symbolSuffix;
            _currentHealth = characterClass.BaseHealth;
            BattleStats = characterClass.BaseStats;
        }


        public ICharacterAction DecideAction(Character target)
        {
            int targetDistance = Position.DistanceFrom(target.Position);

            IReadOnlyList<ICharacterAction> validActions = _characterClass.GetValidActions(targetDistance);

            return validActions.GetRandomElement();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Position.Vacate();
            IsDead = true;
        }

        public void Attack(Character target)
        {
            
        }

        

    }
}

// -> Fiz uma abtração das classes com a interface CharacterClass
// -> Movimento virou uma ação do character, assim como ataque e ataques especiais
// -> Tirei o playerIndex e transformei em um DisplaySymbol para diferenciar eles.
