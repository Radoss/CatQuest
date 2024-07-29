using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string Id;
    public string Name;
    public Sprite Icon;
    public int Stack;
    public bool IsUsedFromInventory;
    public int MaxSizeOfStack;
    public bool IsReusable;
    public virtual void Use()
    {
        Debug.Log("Used Item " + Name);
    }
}
