using UnityEngine;
using System;

public class Wallet : MonoBehaviour, IDataLoader, IDataSaver
{
    [SerializeField] private int _money = 0;
    public int Money { get { return _money; } }

    public event Action<int> OnMoneyChanged;

    private void Start()
    {
        MoneyGiver.OnMoneyGiven += MoneyGiver_OnMoneyGiven;
    }

    private void OnDestroy()
    {
        MoneyGiver.OnMoneyGiven -= MoneyGiver_OnMoneyGiven;
    }

    private void MoneyGiver_OnMoneyGiven(int moneyToAdd)
    {
        _money += moneyToAdd;
        OnMoneyChanged?.Invoke(moneyToAdd);
    }

    public bool HasEnoughMoneyForItem(SellableItemSO sellableItem)
    {
        return sellableItem.Price <= _money;
    }

    public bool PayForItem(SellableItemSO sellableItem)
    {
        if (!HasEnoughMoneyForItem(sellableItem))
        {
            return false;
        }
        _money -= sellableItem.Price;
        OnMoneyChanged?.Invoke(-sellableItem.Price);
        return true;
    }

    public void LoadData(GameData gameData)
    {
        _money = gameData.Money; 
    }

    public void SaveData(GameData gameData)
    {
        gameData.Money = _money;
    }
}
