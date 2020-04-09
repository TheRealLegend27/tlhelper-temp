using System;
using System.Drawing;
using TLHelper.Stats.Skills;
using static TLHelper.Coords;

namespace TLHelper.Stats
{
    class SkillStats
    {
        private static Position[] skillSlots = new Position[7];

        public static void Init()
        {
            skillSlots[0] = new Position(927, 1002);
            skillSlots[1] = new Position(993, 1002);
            skillSlots[2] = new Position(658, 1001);
            skillSlots[3] = new Position(725, 1001);
            skillSlots[4] = new Position(791, 1001);
            skillSlots[5] = new Position(858, 1001);
            skillSlots[6] = new Position(1959, 1000);
        }

        public static Color GetPxlColor(Skill skill)
        {
            int slotId = skill.SkillSlot;
            (Color c, bool success) = ScreenTools.GetPixelColor(skillSlots[slotId].x, skillSlots[slotId].y);
            if (!success)
            {
                return Color.Transparent;
            }
            else
            {
                return c;
            }
        }

    }
}
