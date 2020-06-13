using System.Drawing;
using System.Xml;
using TLHelper.HotKeys;
using TLHelper.UI.Containers;
using static TLHelper.Skills.AvailableFunctions;

namespace TLHelper.Skills
{
    public class Skill
    {
        private readonly string name;
        public Key Key { get; private set; }
        public int Slot { get; private set; }
        private readonly Image icon;
        public bool active;

        private readonly AvailableType Type;
        public AvailableFunction CanPress;

        public Skill(string name, Image icon, Key key, int slot, bool active, AvailableType type)
        {
            this.name = name;
            this.Key = key;
            this.icon = icon;
            this.Type = type;
            Slot = slot;
            this.active = active;
            CanPress = GetFunction(type);
        }

        public SkillBar GetSkillBar(bool even = false) => new SkillBar(name, icon, Key, Slot, IsActive, this, even);

        public void SetXmlAttribs(XmlElement e)
        {
            e.SetAttribute("slot", Slot.ToString());
            e.SetAttribute("key", ((int)Key.CurrentKey).ToString());
            e.SetAttribute("active", IsActive.ToString());
            e.SetAttribute("func", AvailableFunctions.StringifyType(Type));
        }

        public void SetKey(Key key) => this.Key = key;
        public void SetSlot(int slot) => this.Slot = slot;
        public void SetActive(bool active) => this.active = active;

        public bool IsActive => this.active && this.Key.IsSet;

    }
}
