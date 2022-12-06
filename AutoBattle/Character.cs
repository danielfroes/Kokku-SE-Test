using System;
using System.Collections.Generic;
using System.Linq;
using AutoBattle.CharacterActions;
using AutoBattle.CharacterClasses;
using static System.Collections.Specialized.BitVector32;

namespace AutoBattle
{
    public class Character : IBattlefieldEntity
    {
        public Position Position { get; set; }
        public string DisplaySymbol { get; }
        public BattleStats BattleStats { get; }
        public bool IsDead {get; private set;}
        public TeamData Team { get;}
        public int CurrentHealth { get; private set;}

        ACharacterClass _characterClass;
        public string _symbol;
       

        public Character(ACharacterClass characterClass, TeamData team, int index)
        {
            _characterClass = characterClass;
            DisplaySymbol = $"{characterClass.DisplaySymbol}{team.DisplaySymbol}({index})";
            CurrentHealth = characterClass.BaseHealth;
            BattleStats = characterClass.BaseStats;
            Team = team;
        }

        public IEnumerable<ICharacterAction> DecideTurnActions(Character target)
        {
            int targetDistance = Position.Distance(target.Position);
            
            List<ICharacterAction> turnActions = new List<ICharacterAction>();
            
            IReadOnlyList<ICharacterAction> skillsInRange = GetSkillsInRange(targetDistance);

            turnActions.Add(skillsInRange.Count > 0 ?
                skillsInRange.GetRandomElement() : 
                _characterClass.DefaultAction ) ;

            if (_characterClass.PassiveAction.IsInRange(targetDistance) && CheckPassiveTrigger())
            {
                turnActions.Add(_characterClass.PassiveAction);
            }

            return turnActions;
        }

        public virtual IReadOnlyList<ICharacterAction> GetSkillsInRange(int targetDistance)
        {
            return _characterClass.Skills.ToList().FindAll(skill => skill.IsInRange(targetDistance));
        }

        public bool CheckPassiveTrigger()
        {
            int rand = RandomUtils.GetRandomNumber(0, 100);
            return rand <= _characterClass.PassiveTriggerChance ;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(CurrentHealth - damage, 0);
            if (CurrentHealth == 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(CurrentHealth + amount, _characterClass.BaseHealth);           
        }

        public void Die()
        {
            Position.Vacate();
            IsDead = true;
        }

        public override string ToString()
        {
            return $"{_characterClass} {DisplaySymbol} of team {Team}";
        }

    }
}

