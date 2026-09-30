using System.Linq;
using PapersTweaks;
using UnityEngine;

class PluginUtils
{

    public static EnemyType GetEnemyType(string name)
    {
        try
        {
            return Resources.FindObjectsOfTypeAll<EnemyType>().Single((EnemyType enemyType) => enemyType.name == name);
        }
        catch
        {
            Plugin.logger.LogError("Failed to find enemy type with name \"" + name + "\", please report this as a bug.");
        }

        return null;
    }

}