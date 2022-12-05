using System.Collections.Generic;
using AutoBattle.CharacterActions;

namespace AutoBattle.CharacterClasses
{
    public class PaladinClass : ACharacterClass
    {
        public override int BaseHealth => 100;
        public override BattleStats BaseStats => _baseStats;
        protected override string DisplayName => "Paladin";
        public override ICharacterAction DefaultAction => _defaultAction;
        public override IReadOnlyList<ICharacterAction> Skills => _skills;
        public override ICharacterAction PassiveAction => _passiveAction;
        public override int PassiveTriggerChance => 20;

        IReadOnlyList<ICharacterAction> _skills = new List<ICharacterAction>
        {
            new MeleeAttack(),
            new ShieldAttack()
        };

        ICharacterAction _defaultAction = new WalkAction();
        ICharacterAction _passiveAction = new HealAction();

        BattleStats _baseStats = new BattleStats(20, 20);

    }
}