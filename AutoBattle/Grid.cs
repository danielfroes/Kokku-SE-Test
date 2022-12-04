using System;
using System.Collections.Generic;
using System.Numerics;

namespace AutoBattle
{
    public class Grid
    {
        List<GridCell> _cells = new List<GridCell>();
        
        int _lines;
        int _columns;
        bool _wasModified;

        public Grid(Size size)
        {
            _lines = size.Width;
            _columns = size.Height;

            for (int y = 0; y < _lines; y++)
            {
                for (int x = 0; x < _columns ; x++)
                {
                    GridCell cell = new GridCell(x, y);

                    cell.OnOccupantChange += OnCellOccupantChange; 

                    _cells.Add(cell);
                }
            }
        }

        private void OnCellOccupantChange()
        {
            _wasModified = true;
        }

        public GridCell GetRandomEmptyCell()
        { 
            List<GridCell> unoccupiedCells = _cells.FindAll(cell => cell.Empty);

            if(unoccupiedCells.IsNullOrEmpty())
            {
                //TODO tratamento de erro
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
                    GridCell cell = GetCell(x, y);

                    Console.Write($"[{cell.OcuppantSymbol}]\t");
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
        public GridCell GetCell(int x, int y)
        {

            //if (coordinate.X >= _grid.Lines || coordinate.Y >= _grid.Columns ||
            //    coordinate.X < 0 || coordinate.Y < 0)
            //{
            //    return false;
            //}

            //TODO mudar isso aqui para ser uma referencia diret
            //(Columns * i + j)
            return _cells.Find(cell => cell.Coordinate.X == x && cell.Coordinate.Y == y);
        }
        public GridCell GetCell(Vector2 coordinate) => GetCell((int) coordinate.X, (int) coordinate.Y);
       

        public bool IsCellEmpty(Vector2 coordinate)
        {
            return GetCell(coordinate)?.Empty ?? false;
        }
    }


    public struct Size
    {
        //TODO: Sera que eu ponho isso aqui como properties?
        public int Width;
        public int Height;

        public bool IsValid()
        {
            return Width > 0 && Height > 0;
        }
    }
}
// -> Troquei a Grid Cell de struct para classe pq ela tem estado mutavel, devido à variavel de ocupado
// -> Criei uma nova classe chamada Battlefield para servir de intermedio entre a interação dos players com a grid,
//      Abstraindo esses comportamentos e tirando essa responsabilidade do própio character e do Game Manager.
// -> Troquei o xIndex e yIndex para um Vector2 Coordinate;