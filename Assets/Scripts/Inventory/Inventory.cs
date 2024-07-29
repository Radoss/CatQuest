using System.Collections.Generic;
using System;

public class Inventory
{
    public static event Action OnItemsChanged;
    protected static List<InventorySlot> _inventorySlots;
    public int NumberOfSlots { get { return _numberOfSlots; } }
    protected int _numberOfSlots;

    public Inventory(int numberOfSlots)
    {
        _numberOfSlots = numberOfSlots;
        GenerateSlots();
    }

    public InventorySlot GetInventorySlot(int slotNumber)
    {
        return _inventorySlots[slotNumber] as InventorySlot;
    }

    public void RemoveItemFromSlot(int slotNumber)
    {
        _inventorySlots[slotNumber].RemoveNumberOfItems(1);
        OnItemsChanged?.Invoke();
    }

    public void AddItem(ItemSO item, int quantity, Action ActionAfterSuccess = null)
    {
        if (PutItemInAvailableSlot(item, quantity))
        {
            OnItemsChanged?.Invoke();
            ActionAfterSuccess?.Invoke();
        }
    }

    public ItemSO GetItemFromSlot(int slotNumber)
    {
        return _inventorySlots[slotNumber].Item;
    }

    public bool HasAvailableSlotsForItem(ItemSO item)
    {
        return GetNextAvailableSlotForItem(item) != null;
    }

    public void ClearSlot(int slotNumber)
    {
        _inventorySlots[slotNumber].ClearSlot();
        OnItemsChanged?.Invoke();
    }

    public static int GetQuantityOfItem(ItemSO item)
    {
        int quantity = 0;
        foreach (InventorySlot inventorySlot in _inventorySlots)
        {
            if (item == inventorySlot.Item)
            {
                quantity += inventorySlot.Quantity;
            }
        }
        return quantity;
    }

    public static void RemoveSomeItems(ItemSO item, int quantity)
    {
        int quantityToRemove = quantity;
        foreach (InventorySlot inventorySlot in _inventorySlots)
        {
            if (item == inventorySlot.Item)
            {
                int quantityToRemoveFromSlot = Math.Clamp(quantityToRemove, 0, inventorySlot.Quantity);
                quantityToRemove -= quantityToRemoveFromSlot;
                inventorySlot.RemoveNumberOfItems(quantityToRemoveFromSlot);
                OnItemsChanged?.Invoke();
                if (quantityToRemove <= 0)
                {
                    return;
                }
            }
        }
    }

    private void GenerateSlots()
    {
        _inventorySlots = new List<InventorySlot>();
        for (int i = 0; i < _numberOfSlots; i++)
        {
            InventorySlot slot = new InventorySlot();
            _inventorySlots.Add(slot);
        }
    }

    private bool PutItemInAvailableSlot(ItemSO item, int quantity)
    {
        int quantityLeft = quantity;
        bool foundSomeSpace = false;
        while (quantityLeft > 0)
        {
            InventorySlot slot = GetNextAvailableSlotForItem(item);
            if (slot == null)
            {
                return foundSomeSpace;
            }
            int MaxLimit = (!slot.IsEmpty) ? slot.RoomLeft : item.Stack;
            int quantityToPutInSlot = Math.Clamp(quantityLeft, 0, MaxLimit);
            if (slot.TryAddItem(item, quantityToPutInSlot))
            {
                quantityLeft -= quantityToPutInSlot;
                foundSomeSpace = true;
            }
        }
        return foundSomeSpace;
    }

    private InventorySlot GetNextAvailableSlotForItem(ItemSO item)
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (slot.IsEmpty ||
                (slot.CheckIfSameItem(item) &&
                !slot.IsFull))
            {
                return slot;
            }
        }

        return null;
    }

    public Dictionary<string, int> GetItemIdToAmountMap()
    {
        Dictionary<string, int> result = new Dictionary<string, int>();
        foreach(InventorySlot slot in _inventorySlots)
        {
            if (!slot.IsEmpty)
            {
                if (!result.ContainsKey(slot.Item.Id))
                {
                    result.Add(slot.Item.Id, GetQuantityOfItem(slot.Item));
                }
            }
        }
        return result;
    }
}
