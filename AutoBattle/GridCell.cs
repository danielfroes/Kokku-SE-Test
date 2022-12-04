using System;
using System.Numerics;

namespace AutoBattle
{
    public class GridCell
    {

        public const string EMPTY_SYMBOL = " ";

        public Vector2 Coordinate { get; }
        public string OcuppantSymbol { get; private set; }
        public bool Empty { get; private set; }

        public event Action OnOccupantChange;

        public GridCell(int x, int y)
        {
            Coordinate = new Vector2(x, y);
            Empty = true;
            OcuppantSymbol = EMPTY_SYMBOL;
        }
        
        public int DistanceFrom(GridCell targetCell)
        {
            return (int)(MathF.Abs(Coordinate.X - targetCell.Coordinate.X) + MathF.Abs(Coordinate.Y - targetCell.Coordinate.Y));
        }

        internal void Vacate()
        {
            Empty = true;
            OcuppantSymbol = EMPTY_SYMBOL;

            OnOccupantChange?.Invoke();
        }

        internal void Occupy(string entitySymbol)
        {
            Empty = false;
            OcuppantSymbol = entitySymbol;

            OnOccupantChange?.Invoke();
        }
    }
}
// -> Troquei a Grid Cell de struct para classe pq ela tem estado mutavel, devido à variavel de ocupado
// -> Criei uma nova classe chamada Battlefield para servir de intermedio entre a interação dos players com a grid,
//      Abstraindo esses comportamentos e tirando essa responsabilidade do própio character e do Game Manager.
// -> Troquei o xIndex e yIndex para um Vector2 Coordinate;