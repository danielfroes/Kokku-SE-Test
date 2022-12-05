using System;

namespace AutoBattle
{
    public struct BattleStats
    {
        const float MULTIPLIER_STAGE = 0.5f;
        const float MAX_STAGE = 6;
        const float MIN_STAGE = 6;

        public int Attack =>  (int) (_attack * CalculateStatMultiplier(_attackStage));
        public int Defense => (int) (_defense * CalculateStatMultiplier(_defenseStage));

        int _attack;
        int _attackStage;

        int _defense;   
        int _defenseStage;

        public BattleStats(int attack, int defense)
        {
            _attack = attack;
            _defense = defense;

            _attackStage = 1;
            _defenseStage = 1;
        }
        public void AddDefenseStage(int stages)
        {
            _defenseStage += stages;
            Math.Clamp(_defenseStage, MIN_STAGE, MAX_STAGE);
        }

        public void AddAttackStage(int stages)
        {
            _attackStage += stages;
            Math.Clamp(_attackStage, MIN_STAGE, MAX_STAGE);
        }

        float CalculateStatMultiplier(int statStage)
        {
            return statStage > 0 ? 1 + MULTIPLIER_STAGE * Math.Abs(statStage) :
               1 / (1 + MULTIPLIER_STAGE * Math.Abs(statStage));
        }


        public int CalculateDamage(int baseDamage, BattleStats target)
        {
            int damage = Attack + baseDamage - target.Defense;
            return damage > 0 ? damage : 1;
        }
    }
}