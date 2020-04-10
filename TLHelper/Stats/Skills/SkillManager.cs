using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLHelper.Stats.Skills
{
    class SkillManager
    {
            
        public static void InitSkills(Dictionary<string, Skill> Skills, Form1 form)
        {
            // BARB
            Skills.Add("overpower", new Skill(form.pbOverpower, form.tbOverpower, form.cbOverpower, form.ddOverpower, "Overpower", "barb_overpower", AvailableFunction.ByColor));
            Skills.Add("shout", new Skill(form.pbShout, form.tbShout, form.cbShout, form.ddShout, "Threatening Shout", "barb_threatening_shout", AvailableFunction.ByColor));
            Skills.Add("sprint", new Skill(form.pbSprint, form.tbSprint, form.cbSprint, form.ddSprint, "Sprint", "barb_sprint", AvailableFunction.ByColor));
            Skills.Add("ip", new Skill(form.pbIP, form.tbIP, form.cbIP, form.ddIP, "Ignore Pain", "barb_ignore_pain", AvailableFunction.ByColor));
            Skills.Add("call", new Skill(form.pbCall, form.tbCall, form.cbCall, form.ddCall, "Call of the Ancients", "barb_call_of_the_ancients", AvailableFunction.ByColor));
            Skills.Add("cry", new Skill(form.pbCry, form.tbCry, form.cbCry, form.ddCry, "War Cry", "barb_war_cry", AvailableFunction.ByColor));
            Skills.Add("berserker", new Skill(form.pbBerserker, form.tbBerserker, form.cbBerserker, form.ddBerserker, "Wrath of the Berserker", "barb_wrath_of_the_berserker", AvailableFunction.ByColor));
            Skills.Add("rage", new Skill(form.pbBerserker, form.tbBerserker, form.cbBerserker, form.ddBerserker, "Battle Rage", "barb_battle_rage", AvailableFunction.ByColor));

            // MONK
            Skills.Add("blind", new Skill(form.pbBlind, form.tbBlind, form.cbBlind, form.ddBlind, "Blinding Flash", "monk_blinding_flash", AvailableFunction.ByColor));
            Skills.Add("breath", new Skill(form.pbBreath, form.tbBreath, form.cbBreath, form.ddBreath, "Breath of Heaven", "monk_breath_of_heaven", AvailableFunction.ByColor));
            Skills.Add("serenity", new Skill(form.pbSerenity, form.tbSerenity, form.cbSerenity, form.ddSerenity, "Serenity", "monk_serenity", AvailableFunction.ByColor));
            Skills.Add("healing", new Skill(form.pbHealing, form.tbHealing, form.cbHealing, form.ddHealing, "Mantra of Healing", "monk_mantra_of_healing", AvailableFunction.Trigger));
            Skills.Add("conviction", new Skill(form.pbConviction, form.tbConviction, form.cbConviction, form.ddConviction, "Mantra of Conviction", "monk_mantra_of_conviction", AvailableFunction.Trigger));
            Skills.Add("ally", new Skill(form.pbAlly, form.tbAlly, form.cbAlly, form.ddAlly, "Mystic Ally", "monk_mystic_ally", AvailableFunction.ByColor));
            Skills.Add("epiphany", new Skill(form.pbEpiphany, form.tbEpiphany, form.cbEpiphany, form.ddEpiphany, "Epiphany", "monk_epiphany", AvailableFunction.ByColor));

            // WIZARD
            Skills.Add("frost_nova", new Skill(form.pbFrostNova, form.tbFrostNova, form.cbFrostNova, form.ddFrostNova, "Frost Nova", "wizard_frost_nova", AvailableFunction.ByColor));
            Skills.Add("diamond", new Skill(form.pbDiamond, form.tbDiamond, form.cbDiamond, form.ddDiamond, "Diamond Skin", "wizard_diamond_skin", AvailableFunction.ByColor));
            Skills.Add("ice_armor", new Skill(form.pbIceArmor, form.tbIceArmor, form.cbIceArmor, form.ddIceArmor, "Ice Armor", "wizard_ice_armor", AvailableFunction.ByColor));
            Skills.Add("storm_armor", new Skill(form.pbStormArmor, form.tbStormArmor, form.cbStormArmor, form.ddStormArmor, "Storm Armor", "wizard_storm_armor", AvailableFunction.ByColor));
            Skills.Add("explosion", new Skill(form.pbExplosion, form.tbExplosion, form.cbExplosion, form.ddExplosion, "Explosive Blast", "wizard_explosive_blast", AvailableFunction.ByColor));
            Skills.Add("weapon", new Skill(form.pbWeapon, form.tbWeapon, form.cbWeapon, form.ddWeapon, "Magic Weapon", "wizard_magic_weapon", AvailableFunction.ByColor));
            Skills.Add("familiar", new Skill(form.pbFamiliar, form.tbFamiliar, form.cbFamiliar, form.ddFamiliar, "Familiar", "wizard_familiar", AvailableFunction.ByColor));
            Skills.Add("energy", new Skill(form.pbEnergy, form.tbEnergy, form.cbEnergy, form.ddEnergy, "Energy Armor", "wizard_energy_armor", AvailableFunction.ByColor));

            // DH
            Skills.Add("smoke", new Skill(form.pbSmoke, form.tbSmoke, form.cbSmoke, form.ddSmoke, "Smoke Screen", "dh_smoke_screen", AvailableFunction.ByColor));
            Skills.Add("preparation", new Skill(form.pbPreparation, form.tbPreparation, form.cbPreparation, form.ddPreparation, "Preparation", "dh_preparation", AvailableFunction.ByColor));
            Skills.Add("knives", new Skill(form.pbKnives, form.tbKnives, form.cbKnives, form.ddKnives, "Fan of Knives", "dh_fan_of_knives", AvailableFunction.ByColor));
            Skills.Add("shadow_power", new Skill(form.pbShadowPower, form.tbShadowPower, form.cbShadowPower, form.ddShadowPower, "Shadow Power", "dh_shadow_power", AvailableFunction.ByColor));
            Skills.Add("companion", new Skill(form.pbCompanion, form.tbCompanion, form.cbCompanion, form.ddCompanion, "Companion", "dh_companion", AvailableFunction.ByColor));
            Skills.Add("rain_of_vengeance", new Skill(form.pbRainOfVengeance, form.tbRainOfVengeance, form.cbRainOfVengeance, form.ddRainOfVengeance, "Rain of Vengeance", "dh_rain_of_vengeance", AvailableFunction.ByColor));
            Skills.Add("vengeance", new Skill(form.pbVengeance, form.tbVengeance, form.cbVengeance, form.ddVengeance, "Vengeance", "dh_vengeance", AvailableFunction.ByColor));

            // Crusader
            Skills.Add("iron_skin", new Skill(form.pbIronSkin, form.tbIronSkin, form.cbIronSkin, form.ddIronSkin, "Iron Skin", "crusader_iron_skin", AvailableFunction.ByColor));
            Skills.Add("provoke", new Skill(form.pbProvoke, form.tbProvoke, form.cbProvoke, form.ddProvoke, "Provoke", "crusader_provoke", AvailableFunction.ByColor));
            Skills.Add("valor", new Skill(form.pbValor, form.tbValor, form.cbValor, form.ddValor, "Laws of Valor", "crusader_laws_of_valor", AvailableFunction.ByColor));
            Skills.Add("justice", new Skill(form.pbJustice, form.tbJustice, form.cbJustice, form.ddJustice, "Laws of Justice", "crusader_laws_of_justice", AvailableFunction.ByColor));
            Skills.Add("condemn", new Skill(form.pbCondemn, form.tbCondemn, form.cbCondemn, form.ddCondemn, "Condemn", "crusader_condemn", AvailableFunction.ByColor));
            Skills.Add("hope", new Skill(form.pbHope, form.tbHope, form.cbHope, form.ddHope, "Laws of Hope", "crusader_laws_of_hope", AvailableFunction.ByColor));
            Skills.Add("akarats", new Skill(form.pbAkarats, form.tbAkarats, form.cbAkarats, form.ddAkarats, "Akarats Champion", "crusader_akarats_champion", AvailableFunction.ByColor));

            // WD
            Skills.Add("horrify", new Skill(form.pbHorrify, form.tbHorrify, form.cbHorrify, form.ddHorrify, "Horrify", "wd_horrify", AvailableFunction.ByColor));
            Skills.Add("soul_harvest", new Skill(form.pbSoulHarvest, form.tbSoulHarvest, form.cbSoulHarvest, form.ddSoulHarvest, "Soul Harvest", "wd_soul_harvest", AvailableFunction.ByColor));
            Skills.Add("sacrifice", new Skill(form.pbSacrifice, form.tbSacrifice, form.cbSacrifice, form.ddSacrifice, "Sacrifice", "wd_sacrifice", AvailableFunction.ByColor));
            Skills.Add("gargantuan", new Skill(form.pbGargantuan, form.tbGargantuan, form.cbGargantuan, form.ddGargantuan, "Gargantuan", "wd_gargantuan", AvailableFunction.ByColor));
            Skills.Add("confusion", new Skill(form.pbConfusion, form.tbConfusion, form.cbConfusion, form.ddConfusion, "Mass Confusion", "wd_mass_confusion", AvailableFunction.ByColor));
            Skills.Add("fetish", new Skill(form.pbFetish, form.tbFetish, form.cbFetish, form.ddFetish, "Fetish Army", "wd_fetish_army", AvailableFunction.ByColor));

            // NECRO
            Skills.Add("skeletal_mages", new Skill(form.pbSkeletalMages, form.tbSkeletalMages, form.cbSkeletalMages, form.ddSkeletalMages, "Skeletal Mages", "necro_skeletal_mages", AvailableFunction.FullEssence));
            Skills.Add("command_skeletons", new Skill(form.pbCommandSkeletons, form.tbCommandSkeletons, form.cbCommandSkeletons, form.ddCommandSkeletons, "Command Skeletons", "necro_command_skeletons", AvailableFunction.Trigger));
            Skills.Add("death_nova", new Skill(form.pbDeathNova, form.tbDeathNova, form.cbDeathNova, form.ddDeathNova, "Death Nova", "necro_death_nova", AvailableFunction.ByColor));
            Skills.Add("devour", new Skill(form.pbDevour, form.tbDevour, form.cbDevour, form.ddDevour, "Devour", "necro_devour", AvailableFunction.Trigger));
            Skills.Add("bone_armor", new Skill(form.pbBoneArmor, form.tbBoneArmor, form.cbBoneArmor, form.ddBoneArmor, "Bone Armor", "necro_bone_armor", AvailableFunction.ByColor));
            Skills.Add("land", new Skill(form.pbLand, form.tbLand, form.cbLand, form.ddLand, "Land of the Dead", "necro_land_of_the_dead", AvailableFunction.ByColor));
            Skills.Add("simulacrum", new Skill(form.pbSimulacrum, form.tbSimulacrum, form.cbSimulacrum, form.ddSimulacrum, "Simulacrum", "necro_simulacrum", AvailableFunction.ByColor));
        }

    }
}
