using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu()]
public class ItemDatabase : ScriptableObject
{
    public List<ItemObject> items;

    [Button("Set IDs")]
    public void SetIDs()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].itemID = i += 1;
        }
    }
}
