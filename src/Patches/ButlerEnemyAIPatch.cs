using System;
using HarmonyLib;

namespace PapersTweaks.Patches
{

    [HarmonyPatch(typeof(ButlerEnemyAI))]
    class ButlerEnemyAIPatch
    {

        [HarmonyPatch(nameof(ButlerEnemyAI.Start))]
        [HarmonyPostfix]
        private static void StartPatch(ButlerEnemyAI __instance)
        {
            Plugin.logger.LogInfo("Butler start patch");

            if (Plugin.BoundConfig.removeButlerBees.Value)
            {
                __instance.buzzingAmbience.playOnAwake = false;
                __instance.buzzingAmbience.mute = true;
                __instance.buzzingAmbience.Stop();
            }

            if (Plugin.BoundConfig.butlerHealth.Value > 0)
            {
                int newHp = Math.Min(Plugin.BoundConfig.butlerHealth.Value, __instance.enemyHP);

                Plugin.logger.LogInfo("Set Butler HP to " + newHp + ", was " + __instance.enemyHP);
                __instance.enemyHP = newHp;
            }
        }

    }
}
