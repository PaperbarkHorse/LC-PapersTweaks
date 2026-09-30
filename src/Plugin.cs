using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PapersTweaks.Patches;

namespace PapersTweaks
{

    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("ainavt.lc.lethalconfig")]
    public class Plugin : BaseUnityPlugin
    {
        public const string modGUID = "horse.paperbark.PapersTweaks";
        public const string modName = "PapersTweaks";
        public const string modVersion = "1.0.0";
        private static Harmony _harmony = new Harmony(modGUID);
        internal static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
        internal static PluginConfig BoundConfig { get; private set; } = null!;

        void Awake()
        {
            BoundConfig = new PluginConfig(Config);

            ApplyPatches();

            logger.LogInfo("Paper's Tweaks " + modVersion + " is loaded! /)");
        }

        private static void ApplyPatches()
        {
            _harmony.PatchAll(typeof(StartOfRoundPatch));
            _harmony.PatchAll(typeof(BushWolfEnemyPatch));
            _harmony.PatchAll(typeof(ButlerEnemyAIPatch));
            _harmony.PatchAll(typeof(ButlerBeesEnemyAIPatch));
        }
    }

}
