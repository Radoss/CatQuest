using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityTMP;
    private SellableItemSO _item;
    private Vendor _vendor;

    public void SetStoreSlot(Vendor vendor, SellableItemSO item)
    {
        _vendor = vendor;
        SetItem(item);
    }

    private void SetItem(SellableItemSO item)
    {
        _item = item;
        _icon.sprite = _item.Icon;
        _icon.preserveAspect = true;
        _quantityTMP.text = _item.Stack.ToString();
    }

    public void OnSellButtonClicked()
    {
        if (_item == null)
        {
            return;
        }
        _vendor.SellItem(_item);
    }

}
