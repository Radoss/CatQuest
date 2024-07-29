using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/ItemList")]
public class ItemsListSO : ScriptableObject
{
    public List<ItemSO> items;
}
