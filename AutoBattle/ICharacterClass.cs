
using System.Collections.Generic;

namespace AutoBattle
{
    public interface ICharacterClass
    {
        string DisplaySymbol { get; }
        string DisplayName { get; }
        BattleStats BaseStats { get; }
        int BaseHealth { get; }

        IReadOnlyList<ICharacterAction> GetValidActions(int targetDistance);
    }

    
    //Paladin = 1,
    //Warrior = 2,
    //Cleric = 3,
    //Archer = 4

}