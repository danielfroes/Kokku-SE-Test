using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AutoBattle
{
    public class Battlefield
    {
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
                PlaceEntityInRandomPosition(entity);
            }
        }

        void PlaceEntityInRandomPosition(IBattlefieldEntity entity)
        {
            //TODO fazer o tratamento de erro
            Position randomPosition = _grid.GetRandomEmptyPosition();

            //Tirar esse print
            Console.Write($"Entity {entity.DisplaySymbol} was placed in {randomPosition.Coordinate}\n");
            PlaceEntity(entity, randomPosition);
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

// -> Troquei a Grid Cell de struct para classe pq ela tem estado mutavel, devido à variavel de ocupado
// -> Criei uma nova classe chamada Battlefield para servir de intermedio entre a interação dos players com a grid,
//      Abstraindo esses comportamentos e tirando essa responsabilidade do própio character e do Game Manager.
//  
//
// -> Criei uma interface para as entidades que vão ficar na campo de batalha, dessa forma já deixando
//      preparado para eventuais novas entitades no jogo
//
// -> Coloquei o método de mover para dentro do battlefield, pq aqui eu tenho acesso aos gridcells e deixava as ações mais simples de fazer
// ;