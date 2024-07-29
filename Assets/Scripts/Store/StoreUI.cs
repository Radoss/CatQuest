using UnityEngine;

public class StoreUI : MonoBehaviour
{
    public bool IsStoreActive { get { return _storeUI_GO.activeSelf; } }
    [SerializeField] private GameObject _storeUI_GO;

    [SerializeField] private GameObject _storeSlotPrefabUI;

    private Vendor _vendor;
    private StoreSlotUI[] _slots;

    private void GenerateSlotsForVendor()
    {
        _slots = new StoreSlotUI[_vendor.NumberOfSellableItems];
        for (int i = 0; i < _vendor.NumberOfSellableItems; i++)
        {
            GameObject slot = Instantiate(_storeSlotPrefabUI, _storeUI_GO.transform);
            _slots[i] = slot.GetComponent<StoreSlotUI>();
            _slots[i].SetStoreSlot(_vendor, _vendor.GetItemNumber(i));
        }
    }

    public void OpenStoreForVendor(Vendor vendor)
    {
        ClearStoreUI();
        _vendor = vendor;
        GenerateSlotsForVendor();
        ToggleStoreInventory(true);
    }

    private void ClearStoreUI()
    {
        foreach (Transform child in _storeUI_GO.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleStoreInventory(bool isOpened)
    {
        _storeUI_GO.SetActive(isOpened);
    }
}
