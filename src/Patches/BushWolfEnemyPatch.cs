using HarmonyLib;

namespace PapersTweaks.Patches
{

    [HarmonyPatch(typeof(BushWolfEnemy))]
    class BushWolfEnemyPatch
    {

        [HarmonyPatch(nameof(BushWolfEnemy.Start))]
        [HarmonyPostfix]
        private static void StartPatch(BushWolfEnemy __instance)
        {
            if (Plugin.BoundConfig.BushWolfHealth.Value > 0)
            {
                Plugin.logger.LogInfo("Set Kidnapper Fox HP to " + Plugin.BoundConfig.BushWolfHealth.Value + ", was " + __instance.enemyHP);
                __instance.enemyHP = Plugin.BoundConfig.BushWolfHealth.Value;
            }
        }

    }
}
