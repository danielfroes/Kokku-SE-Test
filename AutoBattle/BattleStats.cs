namespace AutoBattle
{
    public struct BattleStats
    {
        public int Attack { get;}
        public int Defense { get;}

        public BattleStats(int attack, int defense)
        {
            Attack= attack; Defense = defense;
        }

        public static int CalculateDamage(int baseDamage, IBattleStatsHolder attacker, IBattleStatsHolder target)
        {
            int damage = attacker.BattleStats.Attack + baseDamage - target.BattleStats.Defense;
            return damage > 0 ? damage : 1;
        }
    }
}