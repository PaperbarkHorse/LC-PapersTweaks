using System.Collections.Generic;
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

    public static SelectableLevel GetSelectableLevel(string name)
    {
        try
        {
            return Resources.FindObjectsOfTypeAll<SelectableLevel>().Single((SelectableLevel level) => level.name == name);
        }
        catch
        {
            Plugin.logger.LogError("Failed to find SelectableLevel with name \"" + name + "\", please report this as a bug.");
        }

        return null;
    }

    public static Item GetItem(string name)
    {
        try
        {
            return Resources.FindObjectsOfTypeAll<Item>().Single((Item item) => item.name == name);
        }
        catch
        {
            Plugin.logger.LogError("Failed to find item with name \"" + name + "\", please report this as a bug.");
        }

        return null;
    }

    public static void AddItemWithRarity(List<SpawnableItemWithRarity> spawnableScrap, string name, int rarity)
    {
        Item item = GetItem(name);

        if (item != null && rarity > 0)
        {
            spawnableScrap.Add(new SpawnableItemWithRarity(GetItem(name), rarity));
        }
    }

}