using System.Drawing;
using TLHelper.SysCom;
using static TLHelper.Coords.Coords;

namespace TLHelper.Skills
{
    public static class SkillCoords
    {
        private static readonly Position[] skillSlots = new Position[6];

        public static void Init()
        {
            skillSlots[0] = new Position(927, 1002);
            skillSlots[1] = new Position(993, 1002);
            skillSlots[2] = new Position(658, 1001);
            skillSlots[3] = new Position(724, 1001);
            skillSlots[4] = new Position(791, 1001);
            skillSlots[5] = new Position(858, 1001);
        }

        public static Color GetPxlColor(Skill skill)
        {
            int slotId = skill.Slot;
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
