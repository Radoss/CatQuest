using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Inventory/Potion")]
public class PotionSO : SellableItemSO
{
    public static event Action<int> OnHealedEvent;
    [SerializeField] private int HPToHeal = 1;

    public override void Use()
    {
        OnHealedEvent?.Invoke(HPToHeal);
        Debug.Log("Healed " + HPToHeal);
    }
}
