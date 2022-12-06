using System;
using System.Collections.Generic;
using System.Numerics;

namespace AutoBattle
{
    public class Grid
    {
        List<Position> _positions = new List<Position>();
        
        int _lines;
        int _columns;
        bool _wasModified;

        public Grid(Size size)
        {
            _lines = size.Height;
            _columns = size.Width;

            for (int y = 0; y < _lines; y++)
            {
                for (int x = 0; x < _columns ; x++)
                {
                    Position position = new Position(x, y);

                    position.OnOccupantChange += OnCellOccupantChange; 

                    _positions.Add(position);
                }
            }
        }

        private void OnCellOccupantChange()
        {
            _wasModified = true;
        }

        public Position GetRandomEmptyPosition()
        { 
            List<Position> unoccupiedCells = _positions.FindAll(cell => cell.Empty);

            if(unoccupiedCells.IsNullOrEmpty())
            {
                return null;
            }

            return unoccupiedCells.GetRandomElement();
        }

        public void Draw()
        {
            if (!_wasModified) return;

            for (int y = _lines - 1; y >= 0; y--)
            {
                Console.Write($"{y}\t");
                for (int x = 0; x < _columns; x++)
                {
                    Position position = GetPosition(x, y);

                    Console.Write($"[{position.OcuppantSymbol}]\t");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.Write("\t");
            for (int x = 0; x < _columns; x++)
            {
                Console.Write($"{x}\t");
            }

            Console.Write(Environment.NewLine + Environment.NewLine);

            _wasModified = false;
        }

        public Position GetPosition(int x, int y)
        {
            if (x >= _columns || y >= _lines ||
                x < 0 || y < 0)
            {
                return null;
            }

            return _positions[_columns * y + x];
        }

        public Position GetPosition(Vector2 coordinate) => GetPosition((int) coordinate.X, (int) coordinate.Y);
       
    }


    public struct Size
    {
        public int Width
        {
            get => _width;
            set => _width = Math.Max(value, 0);
        }

        public int Height
        {
            get => _height;
            set => _height = Math.Max(value, 0);
        }

        int _width;
        int _height;

        public int Area() => Width * Height; 
    }
}
