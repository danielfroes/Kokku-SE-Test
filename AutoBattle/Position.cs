using System;
using System.Numerics;

namespace AutoBattle
{
    public class Position
    {
        public const string EMPTY_SYMBOL = " ";

        public Vector2 Coordinate { get; }
        public string OcuppantSymbol { get; private set; }
        public bool Empty { get; private set; }

        public event Action OnOccupantChange;

        public Position(int x, int y)
        {
            Coordinate = new Vector2(x, y);
            Empty = true;
            OcuppantSymbol = EMPTY_SYMBOL;
        }
        
        public int Distance(Position target)
        {
            return (int) (MathF.Abs(Coordinate.X - target.Coordinate.X) + MathF.Abs(Coordinate.Y - target.Coordinate.Y));
        }

        public Vector2 DirectionTo(Position target)
        {
            return new Vector2(Math.Sign(target.Coordinate.X - Coordinate.X),
                        Math.Sign(target.Coordinate.Y - Coordinate.Y));
        }

        public void Vacate()
        {
            Empty = true;
            OcuppantSymbol = EMPTY_SYMBOL;

            OnOccupantChange?.Invoke();
        }

        public void Occupy(string entitySymbol)
        {
            Empty = false;
            OcuppantSymbol = entitySymbol;

            OnOccupantChange?.Invoke();
        }

        public override string ToString()
        {
            return Coordinate.ToString();
        }
    }
}
