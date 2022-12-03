using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int _width;
        public int _height;
        public Grid(GridSize size)
        {
            _width = size.Width;
            _height = size.Height;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (_height * i + j));
                    grids.Add(newBox);
                    //Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox currentgrid = new GridBox();
                    if (currentgrid.ocupied)
                    {
                        //if()
                        Console.Write("[X]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }

    public struct GridSize
    {

        //TODO: Sera que eu ponho isso aqui como properties?
        public int Width;
        public int Height;

        public bool IsValid()
        {
            return Width > 0 && Height > 0;
        }
    }
    public struct GridBox
    {
        public int xIndex;
        public int yIndex;
        public bool ocupied;
        public int Index;

        public GridBox(int x, int y, bool ocupied, int index)
        {
            xIndex = x;
            yIndex = y;
            this.ocupied = ocupied;
            this.Index = index;
        }

    }
}
