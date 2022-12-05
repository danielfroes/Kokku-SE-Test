using System.Collections.Generic;
using AutoBattle.CharacterActions;

namespace AutoBattle.CharacterClasses
{
    public class WitchClass : ACharacterClass
    {
        public override int BaseHealth => 30;
        public override BattleStats BaseStats => _baseStats;
        protected override string DisplayName => "Witch";
        public override ICharacterAction DefaultAction => _defaultAction;
        public override IReadOnlyList<ICharacterAction> Skills => _skills;
        public override ICharacterAction PassiveAction => _passiveAction;
        public override int PassiveTriggerChance => 10;

        IReadOnlyList<ICharacterAction> _skills = new List<ICharacterAction>
        {
            new ScareAction(),
            new CurseAction(),
        };

        ICharacterAction _defaultAction = new TeleportAction();

        ICharacterAction _passiveAction = new TeleportTargetAction();

        BattleStats _baseStats = new BattleStats(5, 10);

    }
}