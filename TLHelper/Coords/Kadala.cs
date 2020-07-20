using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLHelper.Settings;
using TLHelper.SysCom;

namespace TLHelper.Coords
{
    public static class Kadala
    {

        private static Dictionary<Items, (string, string)> itemNames = new Dictionary<Items, (string, string)>();

        private static readonly Point[] slots = new Point[12];
        private static readonly Point[] tabs = new Point[3];

        static Kadala()
        {
            // SETUP ITEMS

            itemNames.Add(Items.NONE, ("Inactive", "none"));
            itemNames.Add(Items.WEAPON_1H, ("1H Weapon", "weapon_1h"));
            itemNames.Add(Items.WEAPON_2H, ("2H Weapon", "weapon_2h"));
            itemNames.Add(Items.QUIVER, ("Quiver", "quiver"));
            itemNames.Add(Items.ORB, ("Orb", "orb"));
            itemNames.Add(Items.MOJO, ("Mojo", "mojo"));
            itemNames.Add(Items.PHYLACTERY, ("Phylactery", "phylactery"));
            itemNames.Add(Items.HELM, ("Helmet", "helm"));
            itemNames.Add(Items.GLOVES, ("Gloves", "gloves"));
            itemNames.Add(Items.BOOTS, ("Boots", "boots"));
            itemNames.Add(Items.CHEST_ARMOR, ("Chest Armor", "chest_armor"));
            itemNames.Add(Items.BELT, ("Belt", "belt"));
            itemNames.Add(Items.SHOULDERS, ("Shoulders", "shoulders"));
            itemNames.Add(Items.PANTS, ("Pants", "pants"));
            itemNames.Add(Items.BRACERS, ("Bracers", "bracers"));
            itemNames.Add(Items.SHIELD, ("Shield", "shield"));
            itemNames.Add(Items.RING, ("Ring", "ring"));
            itemNames.Add(Items.AMULET, ("Amulet", "amulet"));

            const int offsetX = 41;
            const int width = 223;

            const int offsetY = 158;
            const int height = 107;

            for (int i = 0; i<6; i++)
            {
                int realIndex = i * 2;
                slots[realIndex] = new Point(offsetX + (width / 2), offsetY + (height * i) + (height / 2));
                slots[realIndex + 1] = new Point(offsetX + width + (width / 2), offsetY + (height * i) + (height / 2));

                Console.WriteLine(realIndex + ": " + slots[realIndex].X + " | " + slots[realIndex].Y);
                Console.WriteLine((realIndex+1) + ": " + slots[realIndex+1].X + " | " + slots[realIndex+1].Y);
            }

            tabs[0] = new Point(512, 226);
            tabs[1] = new Point(512, 354);
            tabs[2] = new Point(512, 481);
        }

        public static Items GetSelectedItem()
        {
            return GetItemByCode(SettingsManager.GetSetting("kadala-gamble"));
        }

        public static string GetItemNameByCode(string code)
        {
            foreach (KeyValuePair<Items, (string, string)> kvp in itemNames)
            {
                if (kvp.Value.Item2 == code) return kvp.Value.Item1;
            }
            return "";
        }

        public static string GetItemCodeByName(string name)
        {
            foreach (KeyValuePair<Items, (string, string)> kvp in itemNames)
            {
                if (kvp.Value.Item1 == name) return kvp.Value.Item2;
            }
            return "";
        }

        public static Items GetItemByCode(string code)
        {
            foreach (KeyValuePair<Items, (string, string)> kvp in itemNames)
            {
                if (kvp.Value.Item2 == code) return kvp.Key;
            }
            return Items.NONE;
        }

        public static string[] GetItemNames()
        {
            string[] names = new string[itemNames.Count];
            int i = 0;
            foreach (KeyValuePair<Items, (string, string)> kvp in itemNames)
            {
                names[i] = kvp.Value.Item1;
                i++;
            }
            return names;
        }

        public static void GambleItem(Items item, int amount = 60)
        {
            int tab = GetTab(item);
            int slot = GetSlot(item);
            if (SelectTab(tab))
            {
                GambleSlot(slot, amount);
            }
        }

        private static bool GambleSlot(int slot, int amount = 60)
        {
            if (slot >= slots.Length) return false;
            int x = slots[slot].X;
            int y = slots[slot].Y;

            for (int i = 0; i < amount; i++)
                HardwareRobot.DoRightClick(x, y, HardwareRobot.ActionTypes.SIMULATE);

            return true;
        }

        private static bool SelectTab(int tab)
        {
            if (tab >= tabs.Length) return false;
            HardwareRobot.DoLeftClick(tabs[tab].X, tabs[tab].Y, HardwareRobot.ActionTypes.SIMULATE);
            return true;
        }

        public enum Items
        {
            NONE,
            WEAPON_1H,
            WEAPON_2H,
            QUIVER,
            ORB,
            MOJO,
            PHYLACTERY,
            HELM,
            GLOVES,
            BOOTS,
            CHEST_ARMOR,
            BELT,
            SHOULDERS,
            PANTS,
            BRACERS,
            SHIELD,
            RING,
            AMULET
        }

        private static int GetSlot(Items item)
        {
            switch (item)
            {
                case Items.WEAPON_1H:
                case Items.HELM:
                case Items.RING:
                    return 0;

                case Items.WEAPON_2H:
                case Items.GLOVES:
                case Items.AMULET:
                    return 1;

                case Items.QUIVER:
                case Items.BOOTS:
                    return 2;

                case Items.ORB:
                case Items.CHEST_ARMOR:
                    return 3;

                case Items.MOJO:
                case Items.BELT:
                    return 4;

                case Items.PHYLACTERY:
                case Items.SHOULDERS:
                    return 5;

                case Items.PANTS:
                    return 6;

                case Items.BRACERS:
                    return 7;

                case Items.SHIELD:
                    return 8;

                default: return -1;
            }
        }

        private static int GetTab(Items item)
        {
            switch (item)
            {
                case Items.WEAPON_1H:
                case Items.WEAPON_2H:
                case Items.QUIVER:
                case Items.ORB:
                case Items.MOJO:
                case Items.PHYLACTERY:
                    return 0;

                case Items.HELM:
                case Items.GLOVES:
                case Items.BOOTS:
                case Items.CHEST_ARMOR:
                case Items.BELT:
                case Items.SHOULDERS:
                case Items.PANTS:
                case Items.BRACERS:
                case Items.SHIELD:
                    return 1;

                case Items.RING:
                case Items.AMULET:
                    return 2;

                default: return -1;
            }
        }

    }
}
