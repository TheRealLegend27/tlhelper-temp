using System;
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
        public Key key { get; private set; }
        public int Slot { get; private set; }
        private readonly Image icon;
        public bool active;

        private AvailableType type;
        public AvailableFunction CanPress;

        public Skill(string name, Image icon, Key key, int slot, bool active, AvailableType type)
        {
            this.name = name;
            this.key = key;
            this.icon = icon;
            this.type = type;
            Slot = slot;
            this.active = active;
            CanPress = GetFunction(type);
        }

        public SkillBar GetSkillBar() => new SkillBar(name, icon, key, Slot, IsActive, this);

        public void SetXmlAttribs(XmlElement e)
        {
            e.SetAttribute("slot", Slot.ToString());
            e.SetAttribute("key", ((int)key.CurrentKey).ToString());
            e.SetAttribute("active", IsActive.ToString());
            e.SetAttribute("func", AvailableFunctions.StringifyType(type));
        }

        public void SetKey(Key key) => this.key = key;
        public void SetSlot(int slot) => this.Slot = slot;
        public void SetActive(bool active) => this.active = active;

        public bool IsActive => this.active && this.key.IsSet;

    }
}
