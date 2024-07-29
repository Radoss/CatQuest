using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryDataPersistence : MonoBehaviour, IDataSaver, IDataLoader
{
    [SerializeField] private ItemsListSO _itemListSO;
    private Inventory _inventory;

    [Inject]
    private void Construct(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void LoadData(GameData gameData)
    {
        Dictionary<string, int> itemsMap = gameData.InventoryItemIdToAmountmap;
        foreach(string itemId in itemsMap.Keys)
        {
            int amount = itemsMap[itemId];
            if (amount > 0)
            {
                ItemSO item = GetItemSOById(itemId);
                if (item != null)
                {
                    _inventory.AddItem(item, amount);
                }
            }
        }
    }

    private ItemSO GetItemSOById(string ItemId)
    {
        foreach(ItemSO itemSO in _itemListSO.items)
        {
            if(itemSO.Id == ItemId)
                return itemSO;
        }
        return null;
    }

    public void SaveData(GameData gameData)
    {
        Dictionary<string, int> itemsMap = _inventory.GetItemIdToAmountMap();
        gameData.InventoryItemIdToAmountmap = itemsMap;
    }

}
