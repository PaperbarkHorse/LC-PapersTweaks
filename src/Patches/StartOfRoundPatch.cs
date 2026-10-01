using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace PapersTweaks.Patches;

[HarmonyPatch(typeof(StartOfRound))]
internal class StartOfRoundPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void StartDineLootPatch()
    {
        if (Plugin.BoundConfig.ImprovedDineLoot.Value == true)
        {
            Plugin.logger.LogInfo("Updating scrap spawns on Dine");

            string[] scrapToRemove = [
                "WhoopieCushion",
                "EasterEgg",
                "SeveredHand",
                "SeveredBone",
                "SeveredBoneRib",
                "SeveredEar",
                "SeveredFoot",
                "SeveredThigh",
                "SeveredHeart",
                "SeveredTongue",
            ];

            SelectableLevel dine = PluginUtils.GetSelectableLevel("DineLevel");

            if (dine != null)
            {
                dine.spawnableScrap.RemoveAll(scrap => scrapToRemove.Contains(scrap.spawnableItem.name));

                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredBone", 200);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredEar", 50);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredFoot", 70);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredHand", 70);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredHeart", 50);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredThigh", 50);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SeveredTongue", 50);

                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "7Ball", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Airhorn", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Bell", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "BottleBin", 50);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Brush", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Candy", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "ChemicalJug", 10);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Clock", 20);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "ClownHorn", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Dentures", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "DustPan", 20);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "EasterEgg", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "EggBeater", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "FancyCup", 10);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "FancyLamp", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "FancyPainting", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "FishTestProp", 50);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "FlashLaserPointer", 15);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Flask", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "GiftBox", 80);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "GoldBar", 1);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Hairdryer", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "MagnifyingGlass", 20);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "MoldPan", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Mug", 20);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "PerfumeBottle", 25);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Phone", 15);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "PickleJar", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "PillBottle", 40);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "PlasticCup", 100);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Remote", 10);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Ring", 20);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "RobotToy", 10);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "RubberDuck", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SoccerBall", 5);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "SodaCanRed", 100);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "ToiletPaperRolls", 10);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "Toothpaste", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "ToyCube", 30);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "ToyTrain", 10);
                PluginUtils.AddItemWithRarity(dine.spawnableScrap, "WhoopieCushion", 30);

                dine.minScrap = 35;
                dine.maxScrap = 48;
                dine.minTotalScrapValue = 600;
                dine.maxTotalScrapValue = 700;
            }
        }

        // Butler spawning
        if (Plugin.BoundConfig.ButlerMaxCount.Value > 0)
        {
            EnemyType butler = PluginUtils.GetEnemyType("Butler");

            if (butler != null)
            {
                Plugin.logger.LogInfo("Set Butler max count to " + Plugin.BoundConfig.ButlerMaxCount.Value);
                butler.MaxCount = Plugin.BoundConfig.ButlerMaxCount.Value;
            }
        }
    }

    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void StartButlerSpawningPatch()
    {
        if (Plugin.BoundConfig.ButlerMaxCount.Value > 0)
        {
            EnemyType butler = PluginUtils.GetEnemyType("Butler");

            if (butler != null)
            {
                Plugin.logger.LogInfo("Set Butler max count to " + Plugin.BoundConfig.ButlerMaxCount.Value);
                butler.MaxCount = Plugin.BoundConfig.ButlerMaxCount.Value;
            }
        }
    }

    [HarmonyPatch(nameof(StartOfRound.SetPlanetsMold))]
    [HarmonyPostfix]
    private static void SetPlanetsMoldPatch(StartOfRound __instance, ref SelectableLevel[] ___levels, ref int ___randomMapSeed)
    {
        if (Plugin.BoundConfig.VainInfestationEnabled.Value && __instance.IsServer)
        {
            RandomiseInfestationMoldSpread(___levels, ___randomMapSeed);
        }
    }

    [HarmonyPatch(nameof(StartOfRound.LoadPlanetsMoldSpreadData))]
    [HarmonyPostfix]
    private static void LoadPlanetsMoldSpreadDataPatch(StartOfRound __instance, ref SelectableLevel[] ___levels, ref int ___randomMapSeed)
    {
        if (Plugin.BoundConfig.VainInfestationEnabled.Value && __instance.IsServer)
        {
            RandomiseInfestationMoldSpread(___levels, ___randomMapSeed);
        }
    }

    private static void RandomiseInfestationMoldSpread(SelectableLevel[] levels, int randomMapSeed)
    {
        Plugin.logger.LogInfo("Randomising vain shroud infestations for all levels");

        System.Random random = new System.Random(randomMapSeed + 32);

        for (int i = 0; i < levels.Length; i++)
        {
            SelectableLevel level = levels[i];

            level.moldStartPosition = -1;

            if (level.canSpawnMold && random.Next(0, 100) < Plugin.BoundConfig.VainInfestationChance.Value)
            {
                level.moldSpreadIterations = random.Next(
                    Plugin.BoundConfig.VainInfestationSizeMin.Value,
                    Plugin.BoundConfig.VainInfestationSizeMax.Value
                );

                Plugin.logger.LogInfo(" - " + level.PlanetName + " is infested with vain shrouds");
            }
            else
            {
                level.moldSpreadIterations = 0;
            }
        }
    }
}
