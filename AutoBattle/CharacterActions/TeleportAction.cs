using System;

namespace AutoBattle.CharacterActions
{
    public class TeleportAction : ICharacterAction
    {
        public bool IsInRange(int targetDistance) => true;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {

            if (battlefield.TryPlaceEntityInRandomPosition(character))
            {
                return $"{character} teleported to position {character.Position}";
            }

            return $"{character} tried to teleport, but it failed.";

        }


    }
}