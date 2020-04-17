using static TLHelper.Coords.Coords;

namespace TLHelper.Player
{
    public class InventoryIterator
    {
        private readonly int rows, cols, itemSize;
        private readonly Position topLeftInv, slot;
        private int currentRow, currentCol;

        public InventoryIterator(int rows, int cols, int itemSize)
        {
            this.rows = rows;
            this.cols = cols;
            this.itemSize = itemSize;

            topLeftInv = TopLeftInv;
            slot = Slot;
        }

        public bool HasNext => !(currentRow >= (rows / itemSize) - 1 && currentCol >= cols);

        public Position GetNext()
        {
            Position next = new Position(topLeftInv.x + (slot.x * currentCol), topLeftInv.y + (slot.y * currentRow * itemSize));
            currentCol++;
            if (currentCol == cols && currentRow < (rows / itemSize) - 1)
            {
                currentCol = 0;
                currentRow++;
            }
            return next;
        }

    }
}
