using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;
using HarmonyLib;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;

namespace PapersTweaks
{
    class PluginConfig
    {
        public readonly ConfigEntry<bool> vainInfestationEnabled;
        public readonly ConfigEntry<int> vainInfestationChance;
        public readonly ConfigEntry<int> vainInfestationSizeMin;
        public readonly ConfigEntry<int> vainInfestationSizeMax;
        public readonly ConfigEntry<int> bushWolfHealth;
        public readonly ConfigEntry<int> butlerHealth;
        public readonly ConfigEntry<int> butlerMaxCount;
        public readonly ConfigEntry<bool> removeButlerBees;

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
                5,
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

            butlerHealth = config.Bind(
                "Tweaks.Butler",
                "Health",
                4,
                "The amount of health Butlers have in multiplayer. Set to 0 to disable this tweak."
            );
            butlerMaxCount = config.Bind(
                "Tweaks.Butler",
                "MaxCount",
                2,
                "The maximum number of Butlers which can spawn. Set to 0 to disable this tweak."
            );
            removeButlerBees = config.Bind(
                "Tweaks.Butler",
                "Remove Butler Bees",
                true,
                "Prevents Butler Bees from spawning when a butler Dies."
            );

            ClearOrphanedEntries(config);
            config.Save();
            config.SaveOnConfigSet = true;

            LethalConfigManager.AddConfigItem(
                new BoolCheckBoxConfigItem(vainInfestationEnabled, new BoolCheckBoxOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Enabled",
                    Description = "When enabled, Vain Shrouds have a random chance of occuring each round instead of vanilla's spawning where they stay between days."
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(vainInfestationChance, new IntSliderOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Chance",
                    Description = "The chance (as a percentage) for a moon to have Vain Shrouds. Enable infestations and set to 0 to completely disable Vain Shroud spawning.",
                    Min = 0,
                    Max = 100
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(vainInfestationSizeMin, new IntSliderOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Min Size",
                    Description = "The smallest amount of Vain Shrouds that can spawn, as the number of spawning iterations.",
                    Min = 0,
                    Max = 20
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(vainInfestationSizeMax, new IntSliderOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Max Size",
                    Description = "The largest amount of Vain Shrouds that can spawn, as the number of spawning iterations.",
                    Min = 0,
                    Max = 20
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(bushWolfHealth, new IntSliderOptions
                {
                    Section = "Kidnapper Fox",
                    Name = "Health",
                    Description = "The amount of health Kidnapper Foxes should spawn with. Set to 0 to disable this tweak and use vanilla's default.",
                    Min = 0,
                    Max = 7
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(butlerHealth, new IntSliderOptions
                {
                    Section = "Butler",
                    Name = "Health",
                    Description = "The amount of health Butlers should spawn with. Set to 0 to disable this tweak and use vanilla's default.",
                    Min = 0,
                    Max = 8
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(butlerMaxCount, new IntSliderOptions
                {
                    Section = "Butler",
                    Name = "Max Spawns",
                    Description = "The maximum number of Butlers which can spawn. Set to 0 to disable this tweak and use vanilla's default.",
                    Min = 0,
                    Max = 7
                })
            );

            LethalConfigManager.AddConfigItem(
                new BoolCheckBoxConfigItem(removeButlerBees, new BoolCheckBoxOptions
                {
                    Section = "Butler",
                    Name = "No Butler Bees",
                    Description = "When enabled, Butlers will not spawn bees when they die."
                })
            );
        }

        static void ClearOrphanedEntries(ConfigFile cfg)
        {
            PropertyInfo orphanedEntriesProp = AccessTools.Property(typeof(ConfigFile), "OrphanedEntries");
            var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(cfg);
            orphanedEntries.Clear();
        }
    }
}
