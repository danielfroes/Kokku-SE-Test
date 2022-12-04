using System;
using System.Collections.Generic;
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
            GridCell randomPosition = _grid.GetRandomEmptyCell();

            Console.Write($"Entity {entity.DisplaySymbol} was placed in {randomPosition.Coordinate}\n");
            PlaceEntity(entity, randomPosition);
        }

        public bool TryPlaceEntity(IBattlefieldEntity entity, Vector2 coordinate)
        {
            if (!IsCoordinateEmpty(coordinate))
                return false;

            GridCell newPosition = _grid.GetCell(coordinate);
            PlaceEntity(entity, newPosition);

            return true;
        }

        void PlaceEntity(IBattlefieldEntity entity, GridCell newPosition)
        {

            if (entity.Position != null)
            {
                GridCell oldPosition = entity.Position;
                oldPosition.Vacate();
            }

            newPosition.Occupy(entity.DisplaySymbol);
            entity.Position = newPosition;
        }

       

        public bool IsCoordinateEmpty(Vector2 coordinate)
        {
            return _grid.IsCellEmpty(coordinate);        
        }

        public void Draw()
        {
            _grid.Draw();
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
//
// ;