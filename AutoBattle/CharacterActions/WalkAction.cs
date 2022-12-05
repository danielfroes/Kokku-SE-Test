using System;
using System.Collections.Generic;
using System.Numerics;

namespace AutoBattle.CharacterActions
{
    internal class WalkAction : ICharacterAction
    {
        const int STEPS = 1;
        public int Range => -1;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            Vector2 targetDirection = character.Position.DirectionTo(target.Position);
          
            if (battlefield.TryMoveEntity(character, targetDirection, STEPS))
            {
                return $"Player {character.DisplaySymbol} walked to position {character.Position}\n";
            }
            
            return $"Player {character.DisplaySymbol} standed still in position {character.Position}\n";
            
        }

        

      


    }
}