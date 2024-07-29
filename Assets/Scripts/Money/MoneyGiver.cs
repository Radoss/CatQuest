using System;
using UnityEngine;

public class MoneyGiver : MonoBehaviour
{
    [SerializeField] private int _money;
    public static event Action<int> OnMoneyGiven;

    public void GiveMoney(int numberOfGivenMoney)
    {
        OnMoneyGiven?.Invoke(numberOfGivenMoney);
    }
    public void GiveMoney()
    {
        OnMoneyGiven?.Invoke(_money);
    }
}
