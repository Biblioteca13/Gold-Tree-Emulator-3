using System;
namespace GoldTree.HabboHotel.Pathfinding
{
    internal struct HeightInfo
    {
        private double[,] mMap;
        private double[,] double_1;
        private double[,] double_2;
        private int mMaxX;
        private int mMaxY;

        public HeightInfo(int MaxX, int MaxY, double[,] Map, double[,] double_4, double[,] double_5)
        {
            this.mMap = Map;
            this.double_2 = double_4;
            this.double_1 = double_5;
            this.mMaxX = MaxX;
            this.mMaxY = MaxY;
        }

        internal double GetState(int x, int y)
        {
            double result;
            if (x >= this.mMaxX || x < 0)
            {
                result = 0.0;
            }
            else
            {
                if (y >= this.mMaxY || y < 0)
                {
                    result = 0.0;
                }
                else
                {
                    result = this.mMap[x, y];
                }
            }
            return result;
        }

        internal double GetMapHeight(int X, int Y)
        {
            return this.double_2[X, Y];
        }
    }
}
