using System.Collections.Generic;
using AutoBattle.CharacterActions;

namespace AutoBattle.CharacterClasses
{
    public class MageClass : ACharacterClass
    {
        public override int BaseHealth => 40;
        public override BattleStats BaseStats => _baseStats;
        protected override string DisplayName => "Mage";
        public override ICharacterAction DefaultAction => _defaultAction;
        public override IReadOnlyList<ICharacterAction> Skills => _skills;
        public override ICharacterAction PassiveAction => _passiveAction;
        public override int PassiveTriggerChance => 5;

        IReadOnlyList<ICharacterAction> _skills = new List<ICharacterAction>
        {
            new RunAwayAction(),
            new KnockbackAction(),
        };

        ICharacterAction _defaultAction = new MagicMissileAttack();

        ICharacterAction _passiveAction = new TeleportAction();

        BattleStats _baseStats = new BattleStats(35, 5);

    }
}