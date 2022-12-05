using System.Numerics;

namespace AutoBattle.CharacterActions
{
    public class WalkAction : ICharacterAction
    {
        const int STEPS = 1;
        public bool IsInRange(int targetDistance) => true;
       
        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            Vector2 targetDirection = character.Position.DirectionTo(target.Position);
          
            if (battlefield.TryMoveEntity(character, targetDirection, STEPS))
            {
                return $"{character} walked to position {character.Position}.";
            }
            
            return $"{character} standed still in position {character.Position}.";
        }

    }
}