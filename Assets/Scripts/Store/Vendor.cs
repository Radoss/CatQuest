using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Vendor : MonoBehaviour, IInteractable
{
    public int NumberOfSellableItems { get { return itemsToSell.Length; } }
    [SerializeField] private SellableItemSO[] itemsToSell;
    private Inventory _playerInventory;
    private StoreUI _storeUI;
    private MessageUI _messageUI;
    private Wallet _wallet;
    private SoundPlayer _soundPlayer;

    [Inject]
    private void Construct(Inventory playerInventory, Wallet wallet, StoreUI storeUI, MessageUI messageUI)
    {
        _playerInventory = playerInventory;
        _wallet = wallet;
        _storeUI = storeUI;
        _messageUI = messageUI;
        _soundPlayer = GetComponent<SoundPlayer>();
    }

    public void Interact()
    {
         if(_soundPlayer != null)
        {
            _soundPlayer.PlaySound();
        }
        _storeUI.OpenStoreForVendor(this);
    }

    public void SellItem(SellableItemSO item)
    {
        if(!_wallet.HasEnoughMoneyForItem(item))
        {
            _messageUI.ShowMessage("Not enough money");
            return;
        }
        if (!_playerInventory.HasAvailableSlotsForItem(item))
        {
            _messageUI.ShowMessage("Not enough space in the inventory");
            return;
        }
        if (_wallet.PayForItem(item))
        {
            _playerInventory.AddItem(item, item.Stack);
        }
    }

    public SellableItemSO GetItemNumber(int index)
    {
        return itemsToSell[index];
    }
}
