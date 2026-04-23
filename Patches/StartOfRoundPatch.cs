using HarmonyLib;
using UnityEngine;

namespace PapersTweaks.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch(nameof(StartOfRound.SetPlanetsMold))]
        [HarmonyPostfix]
        private static void SetPlanetsMoldPatch(StartOfRound __instance, ref SelectableLevel[] ___levels, ref int ___randomMapSeed)
        {
            if(Plugin.BoundConfig.vainInfestationEnabled.Value && __instance.IsServer)
            {
                RandomiseInfestationMoldSpread(___levels, ___randomMapSeed);
            }
        }

        [HarmonyPatch(nameof(StartOfRound.LoadPlanetsMoldSpreadData))]
        [HarmonyPostfix]
        private static void LoadPlanetsMoldSpreadDataPatch(StartOfRound __instance, ref SelectableLevel[] ___levels, ref int ___randomMapSeed)
        {
            if (Plugin.BoundConfig.vainInfestationEnabled.Value && __instance.IsServer)
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

                if (level.canSpawnMold && random.Next(0, 100) < Plugin.BoundConfig.vainInfestationChance.Value)
                {
                    level.moldSpreadIterations = random.Next(
                        Plugin.BoundConfig.vainInfestationSizeMin.Value,
                        Plugin.BoundConfig.vainInfestationSizeMax.Value
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
}
