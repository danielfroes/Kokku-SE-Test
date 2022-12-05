using System.Numerics;

namespace AutoBattle.CharacterActions
{
    public class RunAwayAction : ICharacterAction
    {
        const int STEPS = 2;
        const int RANGE = 2;
        public bool IsInRange(int targetDistance)
        {
            return targetDistance <= RANGE;
        }

        public string Execute(Character character, Character target, Battlefield battlefield)
        {

            Vector2 runAwayDirection = - character.Position.DirectionTo(target.Position);

            if (battlefield.TryMoveEntity(character, runAwayDirection, STEPS))
            {
                return $"{character} runned from {target} to position {character.Position}";
            }

            return $"{character} standed still in position {character.Position}";

        }
    }
}