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
        if (Plugin.BoundConfig.ImprovedDineLoot.Value != true) return;

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
        if (dine == null) return;

        dine.spawnableScrap.RemoveAll(scrap => scrapToRemove.Contains(scrap.spawnableItem.name));

        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredBone", 200);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredEar", 50);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredFoot", 70);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredHand", 70);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredHeart", 50);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredThigh", 50);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SeveredTongue", 50);

        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "7Ball", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Airhorn", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Bell", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "BottleBin", 50);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Brush", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Candy", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "ChemicalJug", 10);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Clock", 20);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "ClownHorn", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Dentures", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "DustPan", 20);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "EasterEgg", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "EggBeater", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "FancyCup", 10);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "FancyLamp", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "FancyPainting", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "FishTestProp", 50);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "FlashLaserPointer", 15);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Flask", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "GiftBox", 80);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "GoldBar", 1);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Hairdryer", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "MagnifyingGlass", 20);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "MoldPan", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Mug", 20);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "PerfumeBottle", 25);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Phone", 15);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "PickleJar", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "PillBottle", 40);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "PlasticCup", 100);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Remote", 10);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Ring", 20);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "RobotToy", 10);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "RubberDuck", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SoccerBall", 5);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "SodaCanRed", 100);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "ToiletPaperRolls", 10);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Toothpaste", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "ToyCube", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "ToyTrain", 10);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "WhoopieCushion", 30);
        PluginUtils.AddScrapSpawn(dine.spawnableScrap, "Zeddog", 1);

        dine.minScrap = 35;
        dine.maxScrap = 48;
        dine.minTotalScrapValue = 600;
        dine.maxTotalScrapValue = 700;
    }

    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void StartEmbrionLootPatch()
    {
        if (Plugin.BoundConfig.ImprovedEmbrionLoot.Value != true) return;

        Plugin.logger.LogInfo("Updating scrap spawns on Embrion");

        string[] scrapToRemove = [
            //
        ];

        SelectableLevel embrion = PluginUtils.GetSelectableLevel("EmbrionLevel");
        if (embrion == null) return;

        embrion.spawnableScrap.RemoveAll(scrap => scrapToRemove.Contains(scrap.spawnableItem.name));

        PluginUtils.AddScrapSpawn(embrion.spawnableScrap, "ChemicalJug", 20);
        PluginUtils.AddScrapSpawn(embrion.spawnableScrap, "GoldBar", 5);
        PluginUtils.AddScrapSpawn(embrion.spawnableScrap, "FancyCup", 10);
        PluginUtils.AddScrapSpawn(embrion.spawnableScrap, "Ring", 10);
        PluginUtils.AddScrapSpawn(embrion.spawnableScrap, "WhoopieCushion", 10);
        PluginUtils.AddScrapSpawn(embrion.spawnableScrap, "Zeddog", 1);

        embrion.minScrap = 30;
        embrion.maxScrap = 40;
        embrion.minTotalScrapValue = 600;
        embrion.maxTotalScrapValue = 800;
    }

    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void StartButlerSpawningPatch()
    {
        if (Plugin.BoundConfig.ButlerMaxCount.Value <= 0) return;

        EnemyType butler = PluginUtils.GetEnemyType("Butler");

        if (butler == null) return;

        Plugin.logger.LogInfo("Set Butler max count to " + Plugin.BoundConfig.ButlerMaxCount.Value);
        butler.MaxCount = Plugin.BoundConfig.ButlerMaxCount.Value;
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
