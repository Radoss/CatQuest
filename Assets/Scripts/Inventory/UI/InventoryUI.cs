using UnityEngine;
using Zenject;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI_GO;

    [SerializeField] private GameObject _slotPrefabUI;

    private InventorySlotUI[] _slots;
    private Inventory _inventory;

    [Inject]
    private void Construct(Inventory inventory)
    {
        _inventory = inventory;
        Inventory.OnItemsChanged += Inventory_OnItemsChanged;
        GenerateSlots();
    }

    private void OnDestroy()
    {
        Inventory.OnItemsChanged -= Inventory_OnItemsChanged;
    }

    private void GenerateSlots()
    {
        _slots = new InventorySlotUI[_inventory.NumberOfSlots];
        for (int i = 0; i < _inventory.NumberOfSlots; i++)
        {
            GameObject slot = Instantiate(_slotPrefabUI, inventoryUI_GO.transform);
            _slots[i] = slot.GetComponent<InventorySlotUI>();
            _slots[i].SetInventory(_inventory);
            _slots[i].SetSlotNumber(i);
        }
    }

    private void PlayerInput_OnToggleInventoryEvent()
    {
        ToggleInventoryUI();
    }

    public void ToggleInventoryUI()
    {
        inventoryUI_GO.SetActive(!inventoryUI_GO.activeSelf);
    }

    private void Inventory_OnItemsChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].DisplayInventorySlot(_inventory.GetInventorySlot(i));
        }
    }
}
