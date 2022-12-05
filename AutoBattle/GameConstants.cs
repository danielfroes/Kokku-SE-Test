using AutoBattle.CharacterClasses;
using System.Collections.Generic;

namespace AutoBattle
{
    public static class GameConstants
    {
        public static readonly IReadOnlyList<ACharacterClass> CHARACTER_CLASSES = new List<ACharacterClass>
        {
            new ArcherClass(),
            new MageClass(),
            new PaladinClass(),
            new WitchClass(),
        };

        public static readonly IReadOnlyList<TeamData> TEAMS = new List<TeamData>
        {
            new TeamData("Humans", "H"),
            new TeamData("Orcs", "O")
        };

    }
 
}
