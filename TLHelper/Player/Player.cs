namespace TLHelper.Player
{
    public static class Player
    {
        public static Inventory Inventory;

        static Player()
        {
            Inventory = new Inventory(Coords.Coords.InvRows, Coords.Coords.InvColumns);
        }

    }
}
