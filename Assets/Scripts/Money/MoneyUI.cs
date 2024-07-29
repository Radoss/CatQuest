using UnityEngine;
using TMPro;
using Zenject;

public class MoneyUI : MonoBehaviour, IDataLoader
{
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    private int _money;
    private Wallet _wallet;

    [Inject]
    private void Costruct(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.OnMoneyChanged += _wallet_OnMoneyGiven;
    }

    private void _wallet_OnMoneyGiven(int addedMoney)
    {
        _money += addedMoney;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _moneyTMP.text = _money.ToString();
    }

    public void LoadData(GameData gameData)
    {
        _money = gameData.Money;
        UpdateVisual();
    }
}
