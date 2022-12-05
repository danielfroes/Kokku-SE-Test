
using System.Collections.Generic;
using AutoBattle.CharacterActions;

namespace AutoBattle.CharacterClasses
{
    public abstract class ACharacterClass
    {
        public string DisplaySymbol => DisplayName[0].ToString().ToLower();
        public abstract BattleStats BaseStats { get; }
        public abstract int BaseHealth { get; }
        public abstract ICharacterAction DefaultAction { get; }
        public abstract ICharacterAction PassiveAction { get; }
        public abstract int PassiveTriggerChance { get; }
        public abstract IReadOnlyList<ICharacterAction> Skills { get; }
        protected abstract string DisplayName { get; }



        public override string ToString()
        {
            return DisplayName;
        }

    }

}