using HarmonyLib;

namespace PapersTweaks.Patches
{

    [HarmonyPatch(typeof(BushWolfEnemy))]
    class BushWolfEnemyPatch
    {

        [HarmonyPatch(nameof(BushWolfEnemy.Awake))]
        [HarmonyPostfix]
        private static void SetPlanetsMoldPatch(BushWolfEnemy __instance)
        {
            if (Plugin.BoundConfig.bushWolfHealth.Value > 0)
            {
                Plugin.logger.LogInfo("Set Kidnapper Fox HP to " + Plugin.BoundConfig.bushWolfHealth.Value + ", was " + __instance.enemyHP);
                __instance.enemyHP = Plugin.BoundConfig.bushWolfHealth.Value;
            }
        }

    }
}
