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
        public readonly ConfigEntry<bool> VainInfestationEnabled;
        public readonly ConfigEntry<int> VainInfestationChance;
        public readonly ConfigEntry<int> VainInfestationSizeMin;
        public readonly ConfigEntry<int> VainInfestationSizeMax;
        public readonly ConfigEntry<int> BushWolfHealth;
        public readonly ConfigEntry<int> ButlerHealth;
        public readonly ConfigEntry<int> ButlerMaxCount;
        public readonly ConfigEntry<bool> RemoveButlerBees;
        public readonly ConfigEntry<bool> ImprovedDineLoot;
        public readonly ConfigEntry<bool> ImprovedEmbrionLoot;

        public PluginConfig(ConfigFile config)
        {
            config.SaveOnConfigSet = false;

            VainInfestationEnabled = config.Bind(
                "Tweaks.VainInfestation",
                "Enabled",
                true,
                "Whether random Vain Shroud infestations should replace the vanilla spreading mechanics"
            );
            VainInfestationChance = config.Bind(
                "Tweaks.VainInfestation",
                "Chance",
                5,
                "The chance a moon is infested each day (0 - 100)"
            );
            VainInfestationSizeMin = config.Bind(
                "Tweaks.VainInfestation",
                "MinSize",
                5,
                "Minimum size of vain shroud patches"
            );
            VainInfestationSizeMax = config.Bind(
                "Tweaks.VainInfestation",
                "MaxSize",
                15,
                "Maximum size of vain shroud patches"
            );

            BushWolfHealth = config.Bind(
                "Tweaks.BushWolf",
                "Health",
                3,
                "The amount of health Kidnapper Foxes have. Set to 0 to disable this tweak."
            );

            ButlerHealth = config.Bind(
                "Tweaks.Butler",
                "Health",
                4,
                "The amount of health Butlers have in multiplayer. Set to 0 to disable this tweak."
            );
            ButlerMaxCount = config.Bind(
                "Tweaks.Butler",
                "MaxCount",
                2,
                "The maximum number of Butlers which can spawn. Set to 0 to disable this tweak."
            );
            RemoveButlerBees = config.Bind(
                "Tweaks.Butler",
                "Remove Butler Bees",
                true,
                "Prevents Butler Bees from spawning when a butler Dies."
            );

            ImprovedDineLoot = config.Bind(
                "Tweaks.Dine",
                "Improved Loot Pool",
                true,
                "Changes the scrap that spawns on Dine to be balanced similar to other moons, making it more feasible to visit."
            );

            ImprovedEmbrionLoot = config.Bind(
                "Tweaks.Embrion",
                "Improved Loot Pool",
                true,
                "Changes the scrap that spawns on Embrion to be balanced similar to other moons, making it more feasible to visit."
            );

            ClearOrphanedEntries(config);
            config.Save();
            config.SaveOnConfigSet = true;

            LethalConfigManager.AddConfigItem(
                new BoolCheckBoxConfigItem(VainInfestationEnabled, new BoolCheckBoxOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Enabled",
                    Description = "When enabled, Vain Shrouds have a random chance of occuring each round instead of vanilla's spawning where they stay between days."
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(VainInfestationChance, new IntSliderOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Chance",
                    Description = "The chance (as a percentage) for a moon to have Vain Shrouds. Enable infestations and set to 0 to completely disable Vain Shroud spawning.",
                    Min = 0,
                    Max = 100
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(VainInfestationSizeMin, new IntSliderOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Min Size",
                    Description = "The smallest amount of Vain Shrouds that can spawn, as the number of spawning iterations.",
                    Min = 0,
                    Max = 20
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(VainInfestationSizeMax, new IntSliderOptions
                {
                    Section = "Vain Shroud Infestations",
                    Name = "Max Size",
                    Description = "The largest amount of Vain Shrouds that can spawn, as the number of spawning iterations.",
                    Min = 0,
                    Max = 20
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(BushWolfHealth, new IntSliderOptions
                {
                    Section = "Kidnapper Fox",
                    Name = "Health",
                    Description = "The amount of health Kidnapper Foxes should spawn with. Set to 0 to disable this tweak and use vanilla's default.",
                    Min = 0,
                    Max = 7
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(ButlerHealth, new IntSliderOptions
                {
                    Section = "Butler",
                    Name = "Health",
                    Description = "The amount of health Butlers should spawn with. Set to 0 to disable this tweak and use vanilla's default.",
                    Min = 0,
                    Max = 8
                })
            );

            LethalConfigManager.AddConfigItem(
                new IntSliderConfigItem(ButlerMaxCount, new IntSliderOptions
                {
                    Section = "Butler",
                    Name = "Max Spawns",
                    Description = "The maximum number of Butlers which can spawn. Set to 0 to disable this tweak and use vanilla's default.",
                    Min = 0,
                    Max = 7
                })
            );

            LethalConfigManager.AddConfigItem(
                new BoolCheckBoxConfigItem(RemoveButlerBees, new BoolCheckBoxOptions
                {
                    Section = "Butler",
                    Name = "No Butler Bees",
                    Description = "When enabled, Butlers will not spawn bees when they die."
                })
            );

            LethalConfigManager.AddConfigItem(
                new BoolCheckBoxConfigItem(ImprovedDineLoot, new BoolCheckBoxOptions
                {
                    Section = "Dine",
                    Name = "Improved Loot Pool",
                    Description = "Changes the scrap that spawns on Dine to be balanced similar to other moons, making it more feasible to visit."
                })
            );

            LethalConfigManager.AddConfigItem(
                new BoolCheckBoxConfigItem(ImprovedDineLoot, new BoolCheckBoxOptions
                {
                    Section = "Embrion",
                    Name = "Improved Loot Pool",
                    Description = "Changes the scrap that spawns on Embrion to be balanced similar to other moons, making it more feasible to visit."
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
