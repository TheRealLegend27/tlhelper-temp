namespace TLHelper.Player
{
    public class Inventory
    {
        public int Rows, Cols;

        public Inventory(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }

        public InventoryIterator Get1SlotIterator() => new InventoryIterator(Rows, Cols, 1);
        public InventoryIterator Get2SlotIterator() => new InventoryIterator(Rows, Cols, 2);
    }
}
