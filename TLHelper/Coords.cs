using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLHelper
{
    class Coords
    {
        public static readonly int NativeHeight = 1440;
        public static readonly int NativeWidth = 3440;

        public static Dictionary<string, Coord> coords = new Dictionary<string, Coord>();
        public static Dictionary<string, DimCoord> dimCoords = new Dictionary<string, DimCoord>();

        public static readonly int InvColumns = 10;
        public static readonly int InvRows = 6;

        public static Position Slot;
        public static Position TopLeftInv;

        public static Position SwitchPagesLeft;
        public static Position SwitchPagesRight;

        public static Position Potion50;

        public static void InitCoords ()
        {
            coords.Add("cube_fill", new Coord(CoordType.LeftBased, 957, 1121));
            coords.Add("cube_transmute", new Coord(CoordType.LeftBased, 315, 1106));
            coords.Add("smith_salvage", new Coord(CoordType.LeftBased, 220, 390));
            coords.Add("cube_switch", new Coord(CoordType.LeftBased, 180, 180));
            coords.Add("drop_item", new Coord(CoordType.MiddleBased, 720, 1720));
            dimCoords.Add("inventory", new DimCoord(CoordType.LeftBased, CoordType.RightBased, 2753, 748, 668, 394));
        }
        public static void ConvertCoords(int width, int height)
        {
            float wwidth = (float)width;
            float wheight = (float)height;

            Dictionary<string, Coord> tmpc = new Dictionary<string, Coord>();
            foreach (KeyValuePair<string,Coord> kvp in coords)
            {
                var cor = kvp.Value;
                int rx = 0, ry;
                if (cor.Type == CoordType.MiddleBased)
                {
                    rx = (int)(cor.BaseX * wheight / NativeHeight + (wwidth - NativeWidth * wheight / NativeHeight) / 2);
                }
                else if (cor.Type == CoordType.LeftBased)
                {
                    rx = (int)(cor.BaseX * (wheight / NativeHeight));
                }
                else if (cor.Type == CoordType.RightBased)
                {
                    rx = (int)(wwidth - (NativeWidth - cor.BaseX) * wheight / NativeHeight);
                }

                ry = (int)Math.Round((double)(cor.BaseY * (wheight / NativeHeight)), 0);

                Coord c = new Coord(cor.Type, cor.BaseX, cor.BaseY);
                c.RealX = rx;
                c.RealY = ry;
                tmpc.Add(kvp.Key, c);
            }
            coords = tmpc;

            Dictionary<string, DimCoord> tmpdc = new Dictionary<string, DimCoord>();
            foreach (KeyValuePair<string, DimCoord> kvp in dimCoords)
            {
                var cor = kvp.Value;
                int rx = 0, ry;
                if (cor.PosType == CoordType.MiddleBased)
                {
                    rx = (int)(cor.BaseX * wheight / NativeHeight + (wwidth - NativeWidth * wheight / NativeHeight) / 2);
                }
                else if (cor.PosType == CoordType.LeftBased)
                {
                    rx = (int)(cor.BaseX * (wheight / NativeHeight));
                }
                else if (cor.PosType == CoordType.RightBased)
                {
                    rx = (int)(wwidth - (NativeWidth - cor.BaseX) * wheight / NativeHeight);
                }
                ry = (int)Math.Round((double)(cor.BaseY * (wheight / NativeHeight)), 0);

                int rw = 0, rh;
                if (cor.DimType == CoordType.MiddleBased)
                {
                    rw = (int)(cor.BaseWidth * wheight / NativeHeight + (wwidth - NativeWidth * wheight / NativeHeight) / 2);
                }
                else if (cor.DimType == CoordType.LeftBased)
                {
                    rw = (int)(cor.BaseWidth * (wheight / NativeHeight));
                }
                else if (cor.DimType == CoordType.RightBased)
                {
                    rw = (int)(wwidth - (NativeWidth - cor.BaseWidth) * wheight / NativeHeight);
                }
                rh = (int)Math.Round((double)(cor.BaseHeight * (wheight / NativeHeight)), 0);

                DimCoord c = new DimCoord(cor.PosType, cor.DimType, cor.BaseX, cor.BaseY, cor.BaseWidth, cor.BaseHeight);
                c.RealX = rx;
                c.RealY = ry;
                c.RealWidth = rw;
                c.RealHeight = rh;
                tmpdc.Add(kvp.Key, c);
            }
            dimCoords = tmpdc;

            Slot = new Position(
                (int)dimCoords["inventory"].RealWidth / InvColumns,
                (int)dimCoords["inventory"].RealHeight / InvRows
                );
            TopLeftInv = new Position(
                dimCoords["inventory"].RealX + Slot.x / 2,
                dimCoords["inventory"].RealY + Slot.y / 2
                );
            SwitchPagesLeft = new Position(
                coords["cube_fill"].RealX - coords["cube_switch"].RealX,
                coords["cube_fill"].RealY
                );
            SwitchPagesRight = new Position(
                coords["cube_fill"].RealX + coords["cube_switch"].RealX,
                coords["cube_fill"].RealY
                );

            Potion50 = new Position(543, 979);

            Console.WriteLine(Slot);

            Console.WriteLine(dimCoords["inventory"].RealX);
            Console.WriteLine(dimCoords["inventory"].RealY);
            Console.WriteLine(dimCoords["inventory"].RealWidth);
            Console.WriteLine(dimCoords["inventory"].RealHeight);
        }

        public struct Position
        {
            public int x, y;
            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public struct Coord
        {
            public CoordType Type;
            public int BaseX;
            public int BaseY;
            public int RealX;
            public int RealY;
            public Coord(CoordType type, int baseX, int baseY)
            {
                Type = type;
                BaseX = RealX = baseX;
                BaseY = RealY = baseY;
            }
        }

        public struct DimCoord
        {
            public CoordType DimType;
            public CoordType PosType;
            public int BaseX;
            public int BaseY;
            public int RealX;
            public int RealY;
            public int BaseWidth;
            public int BaseHeight;
            public int RealWidth;
            public int RealHeight;
            public DimCoord(CoordType dimType, CoordType posType, int baseX, int baseY, int baseW, int baseH)
            {
                DimType = dimType;
                PosType = posType;
                BaseX = RealX = baseX;
                BaseY = RealY = baseY;
                BaseWidth = RealWidth = baseW;
                BaseHeight = RealHeight = baseH;
            }
        }

        public enum CoordType
        {
            MiddleBased, LeftBased, RightBased
        }
    }
}