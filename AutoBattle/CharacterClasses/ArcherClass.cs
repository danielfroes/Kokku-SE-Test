using AutoBattle.CharacterActions;
using System;
using System.Collections.Generic;

namespace AutoBattle.CharacterClasses
{
    public class ArcherClass : ACharacterClass
    {
        public override BattleStats BaseStats => _baseStats;
        public override int BaseHealth => 70;
        protected override string DisplayName => "Archer";
        public override ICharacterAction DefaultAction => _defaultAction;
        public override IReadOnlyList<ICharacterAction> Skills => _skills;
        public override ICharacterAction PassiveAction => _passiveAction;
        public override int PassiveTriggerChance => 30;

        ICharacterAction _defaultAction = new WalkAction();
        ICharacterAction _passiveAction = new FocusAction();

        IReadOnlyList<ICharacterAction> _skills = new List<ICharacterAction>
        {
            new RangedAttack(),
            new RollbackAttack()
        };

        BattleStats _baseStats = new BattleStats(25, 10);

    }
}
