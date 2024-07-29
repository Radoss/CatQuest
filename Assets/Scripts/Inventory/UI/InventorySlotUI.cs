using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _removeStackButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private TextMeshProUGUI _quantityTMP;
    private ItemSO _item;
    private int _slotNumber;
    private Inventory _inventory;

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void SetSlotNumber(int number)
    {
        _slotNumber = number;
    }

    public void DisplayInventorySlot(InventorySlot inventorySlot)
    {
        SetActiveElements(true);
        if (!inventorySlot.IsEmpty)
        {
            SyncSlotInfo(inventorySlot);
            return;
        }
        ClearSlot();
    }

    private void SyncSlotInfo(InventorySlot inventorySlot)
    {
        _item = inventorySlot.Item;
        _icon.sprite = _item.Icon;
        _icon.preserveAspect = true;
        _quantityTMP.text = inventorySlot.Quantity.ToString();
    }

    public void OnRemoveStackButtonClicked()
    {
        _inventory.ClearSlot(_slotNumber);
    }

    public void ClearSlot()
    {
        SetActiveElements(false);
        _item = null;
        _icon.sprite = null;
        _quantityTMP.text = "";
    }

    public void OnUseButtonClicked()
    {
        if (!_item.IsUsedFromInventory)
        {
            return;
        }

        _item.Use();

        if (!_item.IsReusable)
        {
            _inventory.RemoveItemFromSlot(_slotNumber);
        }
    }

    private void SetActiveElements(bool isActive)
    {
        _removeStackButton.gameObject.SetActive(isActive);
        _useButton.gameObject.SetActive(isActive);
    }

}
