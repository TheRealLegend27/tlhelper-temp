using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TLHelper.Skills.AvailableFunctions;

namespace TLHelper.Skills
{
    static class DefaultSkills
    {

        public static Dictionary<string, (string, AvailableType)[]> Skills = new Dictionary<string, (string, AvailableType)[]>() {
            {
                "barb", new (string, AvailableType)[] {
                    ("overpower", AvailableType.InActive),
                    ("battle_rage", AvailableType.InActive),
                    ("threatening_shout", AvailableType.InActive),
                    ("sprint", AvailableType.InActive),
                    ("ignore_pain", AvailableType.InActive),
                    ("call_of_the_ancients", AvailableType.InActive),
                    ("war_cry", AvailableType.Trigger),
                    ("wrath_of_the_berserker", AvailableType.InActive),
                }
            },
            {
                "crus", new (string, AvailableType)[]
                {
                    ("iron_skin", AvailableType.InActive),
                    ("provoke", AvailableType.InActive),
                    ("laws_of_valor", AvailableType.InActive),
                    ("laws_of_justice", AvailableType.InActive),
                    ("laws_of_hope", AvailableType.InActive),
                    ("condemn", AvailableType.InActive),
                    ("akarats_champion", AvailableType.InActive),
                }
            },
            {
                "dh", new (string, AvailableType)[]
                {
                    ("smoke_screen", AvailableType.InActive),
                    ("preparation", AvailableType.InActive),
                    ("fan_of_knives", AvailableType.InActive),
                    ("shadow_power", AvailableType.InActive),
                    ("companion", AvailableType.InActive),
                    ("rain_of_vengeance", AvailableType.InActive),
                    ("vengeance", AvailableType.InActive),
                }
            },
            {
                "monk", new (string, AvailableType)[]
                {
                    ("blinding_flash", AvailableType.InActive),
                    ("breath_of_heaven", AvailableType.InActive),
                    ("serenity", AvailableType.InActive),
                    ("mantra_of_healing", AvailableType.Trigger),
                    ("mantra_of_conviction", AvailableType.Trigger),
                    ("mystical_ally", AvailableType.InActive),
                    ("epiphany", AvailableType.InActive),
                }
            },
            {
                "nec", new (string, AvailableType)[]
                {
                    ("skeletal_mages", AvailableType.SkeletalMage),
                    ("command_skeletons", AvailableType.Trigger),
                    ("death_nova", AvailableType.InActive),
                    ("devour", AvailableType.Trigger),
                    ("bone_armor", AvailableType.Trigger),
                    ("land_of_the_dead", AvailableType.InActive),
                    ("simulacrum", AvailableType.InActive),
                }
            },
            {
                "wd", new (string, AvailableType)[]
                {
                    ("horrify", AvailableType.InActive),
                    ("soul_harvest", AvailableType.InActive),
                    ("sacrifice", AvailableType.InActive),
                    ("gargantuan", AvailableType.InActive),
                    ("massconfusion", AvailableType.InActive),
                    ("fetish_army", AvailableType.InActive),
                }
            },
            {
                "wiz", new (string, AvailableType)[]
                {
                    ("frost_nova", AvailableType.InActive),
                    ("diamond_skin", AvailableType.InActive),
                    ("ice_armor", AvailableType.InActive),
                    ("storm_armor", AvailableType.InActive),
                    ("explosive_blast", AvailableType.Trigger),
                    ("magic_weapon", AvailableType.InActive),
                    ("familiar", AvailableType.InActive),
                    ("energy_armor", AvailableType.InActive),
                }
            }
        };

    }
}
