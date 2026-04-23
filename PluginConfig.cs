using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx.Configuration;
using HarmonyLib;

namespace PapersTweaks
{
    class PluginConfig
    {
        public readonly ConfigEntry<bool> vainInfestationEnabled;
        public readonly ConfigEntry<int> vainInfestationChance;
        public readonly ConfigEntry<int> vainInfestationSizeMin;
        public readonly ConfigEntry<int> vainInfestationSizeMax;
        public readonly ConfigEntry<int> bushWolfHealth;

        public PluginConfig(ConfigFile config)
        {
            config.SaveOnConfigSet = false;

            vainInfestationEnabled = config.Bind(
                "Tweaks.VainInfestation",
                "Enabled",
                true,
                "Whether random Vain Shroud infestations should replace the vanilla spreading mechanics"
            );
            vainInfestationChance = config.Bind(
                "Tweaks.VainInfestation",
                "Chance",
                3,
                "The chance a moon is infested each day (0 - 100)"
            );
            vainInfestationSizeMin = config.Bind(
                "Tweaks.VainInfestation",
                "MinSize",
                5,
                "Minimum size of vain shroud patches"
            );
            vainInfestationSizeMax = config.Bind(
                "Tweaks.VainInfestation",
                "MaxSize",
                15,
                "Maximum size of vain shroud patches"
            );

            bushWolfHealth = config.Bind(
                "Tweaks.BushWolf",
                "Health",
                3,
                "The amount of health Kidnapper Foxes have. Set to 0 to disable this tweak."
            );

            ClearOrphanedEntries(config);
            config.Save();
            config.SaveOnConfigSet = true;
        }

        static void ClearOrphanedEntries(ConfigFile cfg)
        {
            PropertyInfo orphanedEntriesProp = AccessTools.Property(typeof(ConfigFile), "OrphanedEntries");
            var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(cfg);
            orphanedEntries.Clear();
        }
    }
}
