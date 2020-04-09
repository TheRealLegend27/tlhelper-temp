using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TLHelper.Coords;

namespace TLHelper
{
    class Inventory
    {
        public static int Rows, Cols;

        public static void Setup()
        {
            Rows = Coords.InvRows;
            Cols = Coords.InvColumns;
        }

        public static InventoryIterator Get1SlotIterator()
        {
            return new InventoryIterator(Rows, Cols, 1);
        }
        public static InventoryIterator Get2SlotIterator()
        {
            return new InventoryIterator(Rows, Cols, 2);
        }
    }

    class InventoryIterator
    {
        private int rows, cols, itemSize;
        private Position topLeftInv, slot;
        private int currentRow, currentCol;

        public InventoryIterator(int rows, int cols, int itemSize)
        {
            this.rows = rows;
            this.cols = cols;
            this.itemSize = itemSize;
            this.topLeftInv = Coords.TopLeftInv;
            this.slot = Coords.Slot;
        }

        public Boolean HasNext()
        {
            return !(currentRow >= (rows / itemSize)-1 && currentCol >= cols);
        }

        public Position getNext()
        {
            Position next = new Position(topLeftInv.x + (slot.x * currentCol), topLeftInv.y + (slot.y * currentRow * itemSize));
            currentCol++;
            if (currentCol == cols && currentRow < (rows / itemSize)-1)
            {
                currentCol = 0;
                currentRow++;
            }
            return next;
        }

    }
}
