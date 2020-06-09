using System.Windows.Forms;
using TLHelper.Skills;

namespace TLHelper.UI.Containers
{
    public class SkillContainer : FlowLayoutPanel
    {
        public SkillContainer()
        {
            FlowDirection = FlowDirection.TopDown;
            Size = UI.Layout.MainControl.SkillContainer.Rect.Size;
            Location = UI.Layout.MainControl.SkillContainer.Rect.Location;
        }

        public void AddSkill(Skill skill) => Controls.Add(skill.GetSkillBar(Controls.Count % 2 == 0));

        public void ClearSkills() => Controls.Clear();
    }
}
