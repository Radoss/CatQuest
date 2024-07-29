using UnityEngine;
using Zenject;
using System;

public class ItemPickup : MonoBehaviour, IInteractable, IDataSaver, IDataLoader
{
    [SerializeField] private string _id;
    [SerializeField] private ItemSO _itemSO;
    [SerializeField] private int _quantity;
    [SerializeField] private GameObject _visualItem;
    private Collider2D _collider;
    private Inventory _inventory;
    private bool IsPickedUp;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }


    [Inject]
    private void Construct(Inventory inventory)
    {
        _inventory = inventory;
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _quantity = (_quantity == 0) ? _itemSO.Stack : _quantity;
    }

    public void Interact()
    {
        _inventory.AddItem(_itemSO, _quantity, AfterPickUpAction);
    }

    private void AfterPickUpAction()
    {
        HideItem();
        IsPickedUp = true;
    }

    private void HideItem()
    {
        _collider.enabled = false;
        _visualItem.SetActive(false);
    }

    public void SaveData(GameData gameData)
    {
        if(IsPickedUp)
        {
            gameData.PickedUpItemIds.Add(_id);
        }
    }

    public void LoadData(GameData gameData)
    {
        if(gameData.PickedUpItemIds.Contains(_id))
        {
            HideItem();
        }
    }
}
