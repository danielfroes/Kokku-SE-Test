using System;
using System.Collections.Generic;
using System.Numerics;

namespace AutoBattle
{
    internal class WalkAction : ICharacterAction
    {
        public int Range => -1;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            Vector2 targetDirection = GetTargetDirection(character.Position, target.Position);
            Vector2? newCoordinate = GetCoordinateToMove(character.Position, targetDirection, battlefield);
            
            if (newCoordinate == null)
            {
                return $"Player {character.DisplaySymbol} standed still in position {character.Position.Coordinate}\n";
            }
   
            battlefield.TryPlaceEntity(character, (Vector2) newCoordinate);
            return $"Player {character.DisplaySymbol} walked to position {newCoordinate}\n";
            //TODO: Pensar se isso vale a pena battlefield.TryMoveEntityToDirection(character, targetDirection);
        }

        private Vector2 GetTargetDirection(GridCell characterPosition, GridCell targetPosition)
        {
            return new Vector2(Math.Sign(targetPosition.Coordinate.X - characterPosition.Coordinate.X),
                        Math.Sign(targetPosition.Coordinate.Y - characterPosition.Coordinate.Y));
        }

        private Vector2? GetCoordinateToMove(GridCell characterPosition, Vector2 targetDirection, Battlefield battlefield)
        {
            Vector2[] possibleCoordinates = new Vector2[]
            {
                characterPosition.Coordinate + targetDirection * Vector2.UnitX,
                characterPosition.Coordinate + targetDirection * Vector2.UnitY
            };

            List<Vector2> emptyCoordinates = new List<Vector2>();

            foreach (Vector2 coordinate in possibleCoordinates)
            {
                if(battlefield.IsCoordinateEmpty(coordinate))
                {
                    emptyCoordinates.Add(coordinate);
                }
            }

            if (emptyCoordinates.Count == 0)
                return null;

            return emptyCoordinates.GetRandomElement(); ;
        }

       
    }
}