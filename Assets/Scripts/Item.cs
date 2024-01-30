using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Mirror,
        SpeedBoost,
    }

    public static int GetCost(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.Mirror: return 1;
            case ItemType.SpeedBoost: return 1;
        }
    }
}
