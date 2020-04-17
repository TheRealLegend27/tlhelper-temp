using TLHelper.Resources;

namespace TLHelper.UI
{
    public static class UIActions
    {
        private static MainForm MainFormRef;
        public static void SetFormRef(MainForm Ref) => MainFormRef = Ref;

        public static void CurrentClassChanged(string selected)
        {
            Skills.SkillManager.SetActiveSkills(GlobalData.Classes.GetKey(selected));
            MainFormRef.OverviewContainer.ChangeTitle(selected == "None" ? "No class Selected" : selected);
        }
    }
}
