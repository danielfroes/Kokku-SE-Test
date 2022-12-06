using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AutoBattle
{
    public class Battlefield
    {
        public const int MIN_AREA_SIZE = 2;

        Grid _grid;

        public Battlefield(Size size)
        {
            _grid = new Grid(size);
        }

        public void Draw()
        {
            _grid.Draw();
        }

        public void PlaceEntitiesRandomly(IEnumerable<IBattlefieldEntity> entities)
        {
            foreach(IBattlefieldEntity entity in entities)
            {
                TryPlaceEntityInRandomPosition(entity);
            }
        }

        public bool TryPlaceEntityInRandomPosition(IBattlefieldEntity entity)
        {
            Position randomPosition = _grid.GetRandomEmptyPosition();
            if(randomPosition == null) 
                return false;

            PlaceEntity(entity, randomPosition);
            return true;
        }


        void PlaceEntity(IBattlefieldEntity entity, Position newPosition)
        {
            if (entity.Position != null)
            {
                Position oldPosition = entity.Position;
                oldPosition.Vacate();
            }

            newPosition.Occupy(entity.DisplaySymbol);
            entity.Position = newPosition;
        }

        public bool TryMoveEntity(IBattlefieldEntity entity, Vector2 direction, int steps)
        {
            Position position = GetEmptyPositionInDirection(entity, direction, steps);
  
            if (position == null)
                return false;
           
            PlaceEntity(entity, position);
            return true;
        }

        private Position GetEmptyPositionInDirection(IBattlefieldEntity entity, Vector2 targetDirection, int preferedSteps)
        {
            List<Position> possiblePositions = new List<Position>();

            for(int i = preferedSteps; i > 0; i--)
            {
                possiblePositions.Add( _grid.GetPosition(
                        entity.Position.Coordinate + Vector2.Normalize(targetDirection * Vector2.UnitX) * preferedSteps)
                    );
                possiblePositions.Add( _grid.GetPosition(
                        entity.Position.Coordinate + Vector2.Normalize(targetDirection * Vector2.UnitY) * preferedSteps)
                    );
            }

            possiblePositions.RemoveAll(cell => cell == null || !cell.Empty);

            if (possiblePositions.Count == 0)
                return null;

            return possiblePositions[0];
        }
    }
}

