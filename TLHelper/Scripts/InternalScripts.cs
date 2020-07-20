using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TLHelper.Coords;
using TLHelper.HotKeys;
using TLHelper.Ingame;
using TLHelper.Player;
using TLHelper.SysCom;
using static TLHelper.Coords.Coords;

namespace TLHelper.Scripts
{
    public static class InternalScripts
    {
        private static Position LastCurser = new Position(0, 0);
        private static void SaveCursor() => LastCurser = new Position(Cursor.Position.X, Cursor.Position.Y);
        private static void RecoverCursor() => Cursor.Position = new Point(LastCurser.x, LastCurser.y);


        public static void RegisterScripts()
        {
            srt.Start();
            slt.Start();

            ScriptManager.AddScript("clear-inv-1", "Clear Inventory (1Slot)", new HotKey(new Key(Keys.D7), true, false, false), true, ScriptOrigins.INT, ClearInv1Space);
            ScriptManager.AddScript("clear-inv-2", "Clear Inventory (2Slot)", new HotKey(new Key(Keys.D8), true, false, false), true, ScriptOrigins.INT, ClearInv2Space);

            ScriptManager.AddScript("do-cube-1", "Cubeconverter (1Slot)", new HotKey(new Key(Keys.D5), true, false, false), true, ScriptOrigins.INT, DoCube1Space);
            ScriptManager.AddScript("do-cube-2", "Cubeconverter (2Slot)", new HotKey(new Key(Keys.D6), true, false, false), true, ScriptOrigins.INT, DoCube2Space);
            ScriptManager.AddScript("cube-reforge", "Cubeconverter (Reforge)", new HotKey(new Key(Keys.D0), true, false, false), true, ScriptOrigins.INT, ReforgeItem);

            ScriptManager.AddScript("drop-inventory", "Drop Inventory", new HotKey(new Key(Keys.J), true, false, false), true, ScriptOrigins.INT, DropItems);
            ScriptManager.AddScript("move-inventory", "Move Inventory", new HotKey(new Key(Keys.I), false, false, true), true, ScriptOrigins.INT, MoveInventory);


            ScriptManager.AddScript("spam-left", "Spam Left", new HotKey(new Key(Keys.LButton), false, false, true), true, ScriptOrigins.INT, SpamLeft);
            ScriptManager.AddScript("spam-right", "Spam Right", new HotKey(new Key(Keys.RButton), false, false, true), true, ScriptOrigins.INT, SpamRight);
        }

        private static readonly int ScriptSleep = 40;
        public static void Sleep(int addMs) => Thread.Sleep(ScriptSleep + addMs);

        public static void ClearInv1Space()
        {
            SaveCursor();
            HardwareListener.UnregisterMouseHooks();

            HardwareRobot.DoLeftClick(coords["smith_salvage"]);
            InventoryIterator inventory = Player.Player.Inventory.Get1SlotIterator();
            while (inventory.HasNext)
            {
                Position p = inventory.GetNext();
                HardwareRobot.MovePhysicalCursor(p);
                HardwareRobot.DoLeftClick(p, HardwareRobot.ActionTypes.PHYSICAL);
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("{ENTER}");
            }

            HardwareListener.RegisterMouseHooks();
            RecoverCursor();
        }

        public static void ClearInv2Space()
        {
            SaveCursor();
            HardwareListener.UnregisterMouseHooks();

            HardwareRobot.DoLeftClick(coords["smith_salvage"]);
            InventoryIterator inventory = Player.Player.Inventory.Get2SlotIterator();
            while (inventory.HasNext)
            {
                Position p = inventory.GetNext();
                HardwareRobot.MovePhysicalCursor(p);
                HardwareRobot.DoLeftClick(p, HardwareRobot.ActionTypes.PHYSICAL);
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("{ENTER}");
            }

            HardwareListener.RegisterMouseHooks();
            RecoverCursor();
        }

        public static void DoCube1Space()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord cubeFill = coords["cube_fill"];
            Coord cubeTransute = coords["cube_transmute"];
            Position cubeLeft = SwitchPagesLeft;
            Position cubeRight = SwitchPagesRight;

            InventoryIterator inventory = Player.Player.Inventory.Get1SlotIterator();
            while (inventory.HasNext)
            {
                Position p = inventory.GetNext();
                HardwareRobot.DoRightClick(p);
                Sleep(0);
                HardwareRobot.DoLeftClick(cubeFill);
                Sleep(0);
                HardwareRobot.DoLeftClick(cubeTransute);
                Sleep(70);
                HardwareRobot.DoLeftClick(cubeRight);
                Sleep(0);
                HardwareRobot.DoLeftClick(cubeLeft);
                Sleep(0);
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();
        }

        public static void DoCube2Space()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord cubeFill = coords["cube_fill"];
            Coord cubeTransute = coords["cube_transmute"];
            Position cubeLeft = SwitchPagesLeft;
            Position cubeRight = SwitchPagesRight;

            InventoryIterator inventory = Player.Player.Inventory.Get2SlotIterator();
            while (inventory.HasNext)
            {
                Position p = inventory.GetNext();
                HardwareRobot.DoRightClick(p);
                Sleep(10);
                HardwareRobot.DoLeftClick(cubeFill);
                Sleep(10);
                HardwareRobot.DoLeftClick(cubeTransute);
                Sleep(60);
                HardwareRobot.DoLeftClick(cubeRight);
                Sleep(10);
                HardwareRobot.DoLeftClick(cubeLeft);
                Sleep(10);
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();
        }

        public static void DropItems()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord Drop = coords["drop_item"];
            InventoryIterator inventory = Player.Player.Inventory.Get1SlotIterator();

            SendKeys.SendWait("i");
            while (inventory.HasNext)
            {
                Position p = inventory.GetNext();
                HardwareRobot.MovePhysicalCursor(p);
                HardwareRobot.DoLeftDown(p.x, p.y, HardwareRobot.ActionTypes.PHYSICAL);
                HardwareRobot.MovePhysicalCursor(Drop);
                Sleep(-20);
                HardwareRobot.DoLeftUp(Drop.RealX, Drop.RealY, HardwareRobot.ActionTypes.PHYSICAL);
            }
            SendKeys.SendWait("i");

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();
        }

        public static void MoveInventory()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            InventoryIterator inventory = Player.Player.Inventory.Get1SlotIterator();
            while (inventory.HasNext)
            {
                Position p = inventory.GetNext();
                HardwareRobot.DoRightClick(p);
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();
        }

        public static void ReforgeItem()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord cubeFill = coords["cube_fill"];
            Coord cubeTransute = coords["cube_transmute"];
            Position cubeLeft = SwitchPagesLeft;
            Position cubeRight = SwitchPagesRight;

            InventoryIterator inventory = Player.Player.Inventory.Get1SlotIterator();
            Position p = inventory.GetNext();
            HardwareRobot.DoRightClick(p.x, p.y);
            Sleep(0);
            HardwareRobot.DoLeftClick(cubeFill);
            Sleep(0);
            HardwareRobot.DoLeftClick(cubeTransute);
            Sleep(40);
            HardwareRobot.DoLeftClick(cubeRight);
            Sleep(0);
            HardwareRobot.DoLeftClick(cubeLeft);
            Sleep(0);

            HardwareRobot.MovePhysicalCursor(p);

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();
        }

        private static readonly EventWaitHandle sltWait = new ManualResetEvent(initialState: false);
        private static readonly EventWaitHandle srtWait = new ManualResetEvent(initialState: false);

        private static readonly Thread slt = new Thread(new ParameterizedThreadStart(SLT));
        private static readonly Thread srt = new Thread(new ParameterizedThreadStart(SRT));

        private static bool sltRunning = false;
        private static bool srtRunning = false;
        public static void SpamLeft()
        {
            if (!sltRunning)
            {
                sltWait.Set();
                sltRunning = true;
            }
        }
        private static void SLT(object obj)
        {
            while (true)
            {
                if (!HardwareListener.IsAltDown)
                {
                    sltWait.Reset();
                    sltRunning = false;
                }
                sltWait.WaitOne();
                HardwareRobot.DoLeftClick();
                Sleep(-25);
            }
        }
        public static void SpamRight()
        {
            if (!srtRunning)
            {
                srtWait.Set();
                srtRunning = true;
            }
        }
        private static void SRT(object obj)
        {
            while (true)
            {
                if (!HardwareListener.IsAltDown)
                {
                    srtWait.Reset();
                    srtRunning = false;
                }
                srtWait.WaitOne();
                HardwareRobot.DoRightClick();
                Sleep(-35);
            }
        }

    }
}
