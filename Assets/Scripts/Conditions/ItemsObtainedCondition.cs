using System.Collections.Generic;

public class ItemsObtainedCondition: ConditionBase
{
    private ItemSO[] _items;
    private int _numberOfEach;

    public ItemsObtainedCondition(ItemSO[] items, int numberOfEach) {
        _items = items;   
        _numberOfEach = numberOfEach;
    }

    public override ConditionCheckStruct ConditionCheck()
    {
        ConditionCheckStruct conditionIsMetMessage = new ConditionCheckStruct();
        conditionIsMetMessage.IsMet = true;
        foreach (ItemSO item in _items)
        {
            if(Inventory.GetQuantityOfItem(item) < _numberOfEach)
            {
                conditionIsMetMessage.IsMet = false;
                conditionIsMetMessage.Message += "<br>" + item.Name + " not found";
            }
        }
        return conditionIsMetMessage;
    }

}
