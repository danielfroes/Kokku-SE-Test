using AutoBattle.CharacterActions;
using System;

namespace AutoBattle.CharacterClasses
{
    internal class TeleportTargetAction : ICharacterAction
    { 
        public bool IsInRange(int targetDistance) => true;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            if (battlefield.TryPlaceEntityInRandomPosition(target))
            {
                return $"{character} teleported {target} to position {character.Position}";
            }

            return $"{character} tried to teleport {target}, but it failed.";
        }

    }
}