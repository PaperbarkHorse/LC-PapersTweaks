using HarmonyLib;

namespace PapersTweaks.Patches
{

    [HarmonyPatch(typeof(ButlerBeesEnemyAI))]
    class ButlerBeesEnemyAIPatch
    {

        [HarmonyPatch(nameof(ButlerBeesEnemyAI.Start))]
        [HarmonyPostfix]
        private static void StartPatch(ButlerBeesEnemyAI __instance)
        {
            if (!__instance.IsServer)
            {
                return;
            }

            if (Plugin.BoundConfig.removeButlerBees.Value)
            {
                if (__instance.thisNetworkObject.IsSpawned)
                {
                    Plugin.logger.LogInfo("ButlerBees forcefully despawned");
                    __instance.thisNetworkObject.Despawn();
                }
                else
                {
                    Plugin.logger.LogInfo("ButlerBees not despawned, they haven't spawned yet");
                }
            }
        }

    }
}
