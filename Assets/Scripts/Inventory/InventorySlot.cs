
public class InventorySlot
{
    public ItemSO Item { get; protected set; }
    public int Quantity { get; protected set; }
    public bool IsEmpty { get { return Item == null; } }
    public bool IsFull { get { return Quantity == Item.MaxSizeOfStack; } }
    public int RoomLeft { get { return Item.MaxSizeOfStack - Quantity; } }

    public bool TryAddItem(ItemSO item, int quantity)
    {
        if (TryFillEmptySlot(item, quantity))
        {
            return true;
        }

        if (!CheckIfSameItem(item) || !CheckIfFits(quantity))
        {
            return false;
        }

        Quantity += quantity;
        return true;
    }

    public bool CheckIfSameItem(ItemSO item)
    {
        return Item == item;
    }

    public void RemoveNumberOfItems(int quantity)
    {
        if (IsEmpty)
        {
            return;
        }
        Quantity -= quantity;
        if (Quantity <= 0)
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        Item = null;
        Quantity = 0;
    }

    private bool TryFillEmptySlot(ItemSO item, int quantity)
    {
        if (IsEmpty)
        {
            Item = item;
            Quantity = quantity;
            return true;
        }
        return false;
    }

    private bool CheckIfFits(int quantity)
    {
        if (IsEmpty)
        {
            return true;
        }
        int newQuantity = Quantity + quantity;
        if (newQuantity > Item.MaxSizeOfStack)
        {
            return false;
        }
        return true;
    }

}
