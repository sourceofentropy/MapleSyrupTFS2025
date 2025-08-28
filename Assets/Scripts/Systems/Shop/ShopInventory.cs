using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShopInventory", order = 1)]
public class ShopInventory : AbstractSOContainer
{
    public Dictionary<ItemTypes.Types, int> inventoryDict; //unity doesn't serialize dictionaries by default...
    public string why = "why";

    [Serializable]
    public class ItemEntry
    {
        public ItemTypes.Types itemType;
        public int quantity;
    }
    [SerializeField] private List<ItemEntry> inventoryList = new List<ItemEntry>(); //they need to be stored in lists to be serialized... may want to change how we're doing this

    public void Initialize()
    {
        if (inventoryDict == null)
        {
            inventoryDict = new Dictionary<ItemTypes.Types, int>();
        }
    }

    private void RebuildDictionary()
    {
        inventoryDict.Clear();
        foreach (var entry in inventoryList)
        {
            inventoryDict[entry.itemType] = entry.quantity;
        }
    }
    /*
    public void SetQuantity(ItemTypes.Types itemType, int quantity)
    {        
        if (inventoryDict == null)
        {
            Initialize();
        }

        if (inventoryDict.ContainsKey(itemType))
        {
            inventoryDict[itemType] = quantity;
        }
        else
        {
            inventoryDict.Add(itemType, quantity);
        }
    }*/

    public void SetQuantity(ItemTypes.Types itemType, int quantity)
    {
        var entry = inventoryList.Find(e => e.itemType == itemType);
        if (entry != null)
        {
            entry.quantity = quantity;
        }
        else
        {
            inventoryList.Add(new ItemEntry { itemType = itemType, quantity = quantity });
        }
        inventoryDict[itemType] = quantity;
    }

    /*
    public void ModifyQuantity(ItemTypes.Types itemType, int quantityChange)
    {
        // Ensure inventoryDict is initialized
        if (inventoryDict == null)
        {
            Initialize();
        }

        UnityEngine.Debug.Log($"modify quantity of {itemType} {quantityChange}");
        foreach (KeyValuePair<ItemTypes.Types, int> item in inventoryDict)
        {
            // item.Key gives you the ItemTypes.Types (the item type)
            // item.Value gives you the quantity (the int value)
            UnityEngine.Debug.Log($"Item: {item.Key}, Quantity: {item.Value}");
        }
        if (inventoryDict.ContainsKey(itemType))
        {
            inventoryList[]
            inventoryDict[itemType] += quantityChange;
        }
    }
    */

    public void ModifyQuantity(ItemTypes.Types itemType, int quantityDelta)
    {
        // Ensure inventoryDict is initialized
        if (inventoryDict == null)
        {
            Initialize();
        }

        var entry = inventoryList.Find(e => e.itemType == itemType);
        if (entry != null)
        {
            entry.quantity += quantityDelta;
        }
        else
        {
            inventoryList.Add(new ItemEntry { itemType = itemType, quantity = quantityDelta });
        }

        // Update the dictionary to stay in sync
        if (inventoryDict.ContainsKey(itemType))
        {
            inventoryDict[itemType] += quantityDelta;
        }
        else
        {
            inventoryDict[itemType] = quantityDelta;
        }
    }

    public int GetQuantity(ItemTypes.Types itemType)
    {
        // Ensure inventoryDict is initialized
        if (inventoryDict == null)
        {
            Initialize();
        }

        if (inventoryDict.ContainsKey(itemType))
        {
            return inventoryDict[itemType];
        }
        else
        {
            return 0;  // Return 0 if item doesn't exist in inventory
        }
    }
}

