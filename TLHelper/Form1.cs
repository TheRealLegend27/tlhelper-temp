using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TLHelper.Hotkeys;
using TLHelper.Stats;
using TLHelper.Stats.Skills;
using static TLHelper.ScreenTools;

namespace TLHelper
{
    public partial class Form1 : Form
    {

        private Dictionary<string, Skill> Skills = new Dictionary<string, Skill>();
        public static Dictionary<string, string> Settings = new Dictionary<string, string>();

        private PotionSkill potionSkill;

        public Form1()
        {
            InitializeComponent();

            HardwareListener.Init(this.Handle);

            Coords.InitCoords();
            (Dim dim, bool success) = ScreenTools.GetWindowDimensions();
            if (!success) MessageBox.Show("Diablo 3 konnte nicht gefunden werden!");
            else
            {
                Coords.ConvertCoords(dim.Width, dim.Height);
            }
            Inventory.Setup();
            SkillStats.Init();

            LoadSettings();
            GenerateEmptySettings();

            ScriptManager.Init();

            ScriptManager.AddScript(new Hotkey(true, false, false, '7'), ScriptManager.ClearInv1Space);
            ScriptManager.AddScript(new Hotkey(true, false, false, '8'), ScriptManager.ClearInv2Space);

            ScriptManager.AddScript(new Hotkey(true, false, false, '5'), ScriptManager.DoCube1Space);
            ScriptManager.AddScript(new Hotkey(true, false, false, '6'), ScriptManager.DoCube2Space);

            ScriptManager.AddScript(new Hotkey(true, false, false, 'j'), ScriptManager.DropItems);

            ScriptManager.AddScript(new Hotkey(false, false, true, 'i'), ScriptManager.MoveInventory);

            ScriptManager.AddScript(new Hotkey(false, false, true, Keys.LButton), ScriptManager.SpamLeft);
            ScriptManager.AddScript(new Hotkey(false, false, true, Keys.RButton), ScriptManager.SpamRight);

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TLHelper/scripts/"))
            {
                string[] scripts = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TLHelper/scripts/");
                foreach (string s in scripts)
                {
                    string file = s.Split('/')[s.Split('/').Length-1];
                    if (file.EndsWith(".tls"))
                    {
                        (string hk, string name, string description) = ScriptManager.InterpretScript(file);
                        tbScripts.Text += hk + " -> " + name + "\r\n";
                    }
                }
            }

            SkillManager.InitSkills(Skills, this);
            potionSkill = new PotionSkill(pbPotion, tbPotion, cbPotion);
        }

        private void SaveSettings()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TLHelper";
            string file = folder + "/config.ini";
                
            if (!Directory.Exists(folder))
            {
                Console.WriteLine("Folder \"TLHelper\" not found");
                Directory.CreateDirectory(folder);
                Console.WriteLine("Folder \"TLHelper\" created");
            }

            // SORT SETTINGS
            Dictionary<string, Dictionary<string,string>> sortedSettings = new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<string,string> kvp in Settings)
            {
                string pref = kvp.Key.Split(new char[] { '_' }, 3)[0] + "_" + kvp.Key.Split(new char[] { '_' }, 3)[1];
                string name = kvp.Key.Split(new char[] { '_' }, 3)[2];

                if (!sortedSettings.ContainsKey(pref))
                    sortedSettings.Add(pref, new Dictionary<string, string>());

                sortedSettings[pref].Add(name, kvp.Value);
            }

            string content = "";
            foreach (KeyValuePair<string,Dictionary<string,string>> category in sortedSettings)
            {
                content += String.Format("[{0}]\n", category.Key);
                foreach (KeyValuePair<string,string> setting in category.Value)
                {
                    content += String.Format("{0}={1}\n", setting.Key, setting.Value);
                }
            }

            if (!File.Exists(file))
            {
                Console.WriteLine("Config File: \"config.ini\" not found");
                FileStream fs = File.Create(file);
                Console.WriteLine("Config File: \"config.ini\" created");

                // GET CONTENT AS BYTE[]
                Byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
                Console.Write("Writing config into \"config.ini\"...  ");
                fs.Write(contentBytes, 0, contentBytes.Length);
                Console.WriteLine(" done");
            }
            else
            {
                Console.Write("Writing config into \"config.ini\"...  ");
                File.WriteAllText(file, content);
                Console.WriteLine(" done");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread mainThread = new Thread(new ThreadStart(RunSkillLoop));
            mainThread.Start();
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            HardwareListener.Action(m);
            base.WndProc(ref m);
        }

        protected void RunSkillLoop()
        {
            while(true)
            {
                SkillBar.ProcessSkills();
                potionSkill.ProcessSkill();
            }
        }

        private void LoadSettings()
        {
            String file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TLHelper/config.ini";
            Console.WriteLine(file);
            if (!File.Exists(file))
            {
                Console.WriteLine("No \"config.ini\" found...");
                return;
            }
            String content = File.ReadAllText(file);

            String[] settings = content.Split('\n');
            String current_prefix = "";
            foreach(String s in settings)
            {
                if (s.StartsWith("[")) current_prefix = s.Substring(1).Split(']')[0];
                else
                {
                    if (s.Length <= 1) continue;
                    string st = s.Replace(" = ", "=").Replace(" =", "=").Replace("= ", "=");
                    var name = st.Split('=')[0];
                    var cont = st.Split('=').Length == 1 ? "" : st.Split('=')[1];

                    var sndTab = "\t";
                    if (name.Length <= 13) sndTab += "\t";
                    Console.WriteLine(String.Format("Loaded \t {0,-25} => {1,-25} ", current_prefix + "_" + name, cont == "" ? "unset" : cont));
                    Settings.Add(String.Format("{0}_{1}", current_prefix, name), cont);
                }
            }

        }

        private void GenerateEmptySettings()
        {
            // DEFAULT SETTINGS
            string defaultKey = "";
            bool defaultActive = false;
            int defaultSlot = -1;

            // REQUIRED FIELDS
            Dictionary<string, string[]> reqFields = new Dictionary<string, string[]>();
            reqFields.Add("barb", new string[]{ "overpower", "battle_rage", "threatening_shout", "sprint", "ignore_pain", "call_of_the_ancients", "war_cry", "wrath_of_the_berserker" });
            reqFields.Add("monk", new string[]{ "blinding_flash", "breath_of_heaven", "serenity", "mantra_of_healing", "mantra_of_conviction", "mystic_ally", "epiphany" });
            reqFields.Add("wizard", new string[]{ "frost_nova", "diamond_skin", "ice_armor", "storm_armor", "explosive_blast", "magic_weapon", "familiar", "energy_armor" });
            reqFields.Add("dh", new string[]{ "smoke_screen", "preparation", "fan_of_knives", "shadow_power", "companion", "rain_of_vengeance", "vengeance" });
            reqFields.Add("crusader", new string[]{ "iron_skin", "provoke", "laws_of_valor", "condemn", "laws_of_hope", "akarats_champion", "laws_of_justice" });
            reqFields.Add("wd", new string[]{ "horrify", "soul_harvest", "sacrifice", "gargantuan", "mass_confusion", "fetish_army" });
            reqFields.Add("necro", new string[]{ "skeletal_mages", "command_skeletons", "death_nova", "devour", "bone_armor", "land_of_the_dead", "simulacrum" });
            reqFields.Add("general", new string[] { "potion" });

            foreach (KeyValuePair<string, string[]> classFields in reqFields)
            {
                string pref = classFields.Key;
                foreach (string baseName in classFields.Value)
                {
                    string key = String.Format("{0}_key_{1}", pref, baseName);
                    string active = String.Format("{0}_active_{1}", pref, baseName);
                    string slot = String.Format("{0}_slot_{1}", pref, baseName);

                    if (!Settings.ContainsKey(key))
                    {
                        PutInSettings(key, defaultKey);
                        Console.WriteLine(String.Format("Missing Setting: {0}, initialized with value: {1}", key, defaultKey == "" ? "unset" : defaultKey));
                    }
                    if (!Settings.ContainsKey(active))
                    {
                        PutInSettings(active, defaultActive);
                        Console.WriteLine(String.Format("Missing Setting: {0}, initialized with value: {1}", active, defaultActive));
                    }
                    if (!Settings.ContainsKey(slot))
                    {
                        PutInSettings(slot, defaultSlot);
                        Console.WriteLine(String.Format("Missing Setting: {0}, initialized with value: {1}", slot, defaultSlot));
                    }
                }
            }
        }

        private void PutInSettings(string key, string value) => Settings.Add(key, value);
        private void PutInSettings(string key, bool value) => Settings.Add(key, value.ToString());
        private void PutInSettings(string key, int value) => Settings.Add(key, value.ToString());

        private void bSave_Click(object sender, EventArgs e)
        {
            // BARB
            Skills["overpower"].key = tbOverpower.Text;
            Skills["overpower"].active = cbOverpower.Checked;
            Skills["overpower"].slot = ddOverpower.SelectedIndex;

            Skills["shout"].key = tbShout.Text;
            Skills["shout"].active = cbShout.Checked;
            Skills["shout"].slot = ddShout.SelectedIndex;

            Skills["sprint"].key = tbSprint.Text;
            Skills["sprint"].active = cbSprint.Checked;
            Skills["sprint"].slot = ddSprint.SelectedIndex;

            Skills["ip"].key = tbIP.Text;
            Skills["ip"].active = cbIP.Checked;
            Skills["ip"].slot = ddIP.SelectedIndex;

            Skills["call"].key = tbCall.Text;
            Skills["call"].active = cbCall.Checked;
            Skills["call"].slot = ddCall.SelectedIndex;

            Skills["cry"].key = tbCry.Text;
            Skills["cry"].active = cbCry.Checked;
            Skills["cry"].slot = ddCry.SelectedIndex;

            Skills["berserker"].key = tbBerserker.Text;
            Skills["berserker"].active = cbBerserker.Checked;
            Skills["berserker"].slot = ddBerserker.SelectedIndex;

            Skills["rage"].key = tbRage.Text;
            Skills["rage"].active = cbRage.Checked;
            Skills["rage"].slot = ddRage.SelectedIndex;

            // MONK
            Skills["blind"].key = tbBlind.Text;
            Skills["blind"].active = cbBlind.Checked;
            Skills["blind"].slot = ddBlind.SelectedIndex;

            Skills["breath"].key = tbBreath.Text;
            Skills["breath"].active = cbBreath.Checked;
            Skills["breath"].slot = ddBreath.SelectedIndex;

            Skills["serenity"].key = tbSerenity.Text;
            Skills["serenity"].active = cbSerenity.Checked;
            Skills["serenity"].slot = ddSerenity.SelectedIndex;

            Skills["healing"].key = tbHealing.Text;
            Skills["healing"].active = cbHealing.Checked;
            Skills["healing"].slot = ddHealing.SelectedIndex;

            Skills["conviction"].key = tbConviction.Text;
            Skills["conviction"].active = cbConviction.Checked;
            Skills["conviction"].slot = ddConviction.SelectedIndex;

            Skills["ally"].key = tbAlly.Text;
            Skills["ally"].active = cbAlly.Checked;
            Skills["ally"].slot = ddAlly.SelectedIndex;

            Skills["epiphany"].key = tbEpiphany.Text;
            Skills["epiphany"].active = cbEpiphany.Checked;
            Skills["epiphany"].slot = ddEpiphany.SelectedIndex;

            // WIZARD
            Skills["frost_nova"].key = tbFrostNova.Text;
            Skills["frost_nova"].active = cbFrostNova.Checked;
            Skills["frost_nova"].slot = ddFrostNova.SelectedIndex;

            Skills["diamond"].key = tbDiamond.Text;
            Skills["diamond"].active = cbDiamond.Checked;
            Skills["diamond"].slot = ddDiamond.SelectedIndex;

            Skills["ice_armor"].key = tbIceArmor.Text;
            Skills["ice_armor"].active = cbIceArmor.Checked;
            Skills["ice_armor"].slot = ddIceArmor.SelectedIndex;

            Skills["storm_armor"].key = tbStormArmor.Text;
            Skills["storm_armor"].active = cbStormArmor.Checked;
            Skills["storm_armor"].slot = ddStormArmor.SelectedIndex;

            Skills["explosion"].key = tbExplosion.Text;
            Skills["explosion"].active = cbExplosion.Checked;
            Skills["explosion"].slot = ddExplosion.SelectedIndex;

            Skills["weapon"].key = tbWeapon.Text;
            Skills["weapon"].active = cbWeapon.Checked;
            Skills["weapon"].slot = ddWeapon.SelectedIndex;

            Skills["familiar"].key = tbFamiliar.Text;
            Skills["familiar"].active = cbFamiliar.Checked;
            Skills["familiar"].slot = ddFamiliar.SelectedIndex;

            Skills["energy"].key = tbEnergy.Text;
            Skills["energy"].active = cbEnergy.Checked;
            Skills["energy"].slot = ddEnergy.SelectedIndex;

            // DH
            Skills["smoke"].key = tbSmoke.Text;
            Skills["smoke"].active = cbSmoke.Checked;
            Skills["smoke"].slot = ddSmoke.SelectedIndex;

            Skills["preparation"].key = tbPreparation.Text;
            Skills["preparation"].active = cbPreparation.Checked;
            Skills["preparation"].slot = ddPreparation.SelectedIndex;

            Skills["knives"].key = tbKnives.Text;
            Skills["knives"].active = cbKnives.Checked;
            Skills["knives"].slot = ddKnives.SelectedIndex;

            Skills["shadow_power"].key = tbShadowPower.Text;
            Skills["shadow_power"].active = cbShadowPower.Checked;
            Skills["shadow_power"].slot = ddShadowPower.SelectedIndex;

            Skills["companion"].key = tbCompanion.Text;
            Skills["companion"].active = cbCompanion.Checked;
            Skills["companion"].slot = ddCompanion.SelectedIndex;

            Skills["rain_of_vengeance"].key = tbRainOfVengeance.Text;
            Skills["rain_of_vengeance"].active = cbRainOfVengeance.Checked;
            Skills["rain_of_vengeance"].slot = ddRainOfVengeance.SelectedIndex;

            Skills["vengeance"].key = tbVengeance.Text;
            Skills["vengeance"].active = cbVengeance.Checked;
            Skills["vengeance"].slot = ddVengeance.SelectedIndex;

            // Crusader
            Skills["iron_skin"].key = tbIronSkin.Text;
            Skills["iron_skin"].active = cbIronSkin.Checked;
            Skills["iron_skin"].slot = ddIronSkin.SelectedIndex;

            Skills["provoke"].key = tbProvoke.Text;
            Skills["provoke"].active = cbProvoke.Checked;
            Skills["provoke"].slot = ddProvoke.SelectedIndex;

            Skills["valor"].key = tbValor.Text;
            Skills["valor"].active = cbValor.Checked;
            Skills["valor"].slot = ddValor.SelectedIndex;

            Skills["justice"].key = tbJustice.Text;
            Skills["justice"].active = cbJustice.Checked;
            Skills["justice"].slot = ddJustice.SelectedIndex;

            Skills["condemn"].key = tbCondemn.Text;
            Skills["condemn"].active = cbCondemn.Checked;
            Skills["condemn"].slot = ddCondemn.SelectedIndex;

            Skills["hope"].key = tbHope.Text;
            Skills["hope"].active = cbHope.Checked;
            Skills["hope"].slot = ddHope.SelectedIndex;

            Skills["akarats"].key = tbAkarats.Text;
            Skills["akarats"].active = cbAkarats.Checked;
            Skills["akarats"].slot = ddAkarats.SelectedIndex;

            // WD
            Skills["horrify"].key = tbHorrify.Text;
            Skills["horrify"].active = cbHorrify.Checked;
            Skills["horrify"].slot = ddHorrify.SelectedIndex;

            Skills["soul_harvest"].key = tbSoulHarvest.Text;
            Skills["soul_harvest"].active = cbSoulHarvest.Checked;
            Skills["soul_harvest"].slot = ddSoulHarvest.SelectedIndex;

            Skills["sacrifice"].key = tbSacrifice.Text;
            Skills["sacrifice"].active = cbSacrifice.Checked;
            Skills["sacrifice"].slot = ddSacrifice.SelectedIndex;

            Skills["gargantuan"].key = tbGargantuan.Text;
            Skills["gargantuan"].active = cbGargantuan.Checked;
            Skills["gargantuan"].slot = ddGargantuan.SelectedIndex;

            Skills["confusion"].key = tbConfusion.Text;
            Skills["confusion"].active = cbConfusion.Checked;
            Skills["confusion"].slot = ddConfusion.SelectedIndex;

            Skills["fetish"].key = tbFetish.Text;
            Skills["fetish"].active = cbFetish.Checked;
            Skills["fetish"].slot = ddFetish.SelectedIndex;

            // WD
            Skills["skeletal_mages"].key = tbSkeletalMages.Text;
            Skills["skeletal_mages"].active = cbSkeletalMages.Checked;
            Skills["skeletal_mages"].slot = ddSkeletalMages.SelectedIndex;

            Skills["command_skeletons"].key = tbCommandSkeletons.Text;
            Skills["command_skeletons"].active = cbCommandSkeletons.Checked;
            Skills["command_skeletons"].slot = ddCommandSkeletons.SelectedIndex;

            Skills["death_nova"].key = tbDeathNova.Text;
            Skills["death_nova"].active = cbDeathNova.Checked;
            Skills["death_nova"].slot = ddDeathNova.SelectedIndex;

            Skills["devour"].key = tbDevour.Text;
            Skills["devour"].active = cbDevour.Checked;
            Skills["devour"].slot = ddDevour.SelectedIndex;

            Skills["bone_armor"].key = tbBoneArmor.Text;
            Skills["bone_armor"].active = cbBoneArmor.Checked;
            Skills["bone_armor"].slot = ddBoneArmor.SelectedIndex;

            Skills["land"].key = tbLand.Text;
            Skills["land"].active = cbLand.Checked;
            Skills["land"].slot = ddLand.SelectedIndex;

            Skills["simulacrum"].key = tbSimulacrum.Text;
            Skills["simulacrum"].active = cbSimulacrum.Checked;
            Skills["simulacrum"].slot = ddSimulacrum.SelectedIndex;

            potionSkill.key = tbPotion.Text;
            potionSkill.active = cbPotion.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(KeyValuePair<string,Skill> kvp in Skills)
            {
                kvp.Value.SaveSettings();
            }
            potionSkill.SaveSettings();
            SaveSettings();
            Console.WriteLine("You can close this Console now...");
        }

        private void cbCurrentClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            SkillBar.SetCurrentClass(cbCurrentClass.SelectedIndex);
        }
    }
}
