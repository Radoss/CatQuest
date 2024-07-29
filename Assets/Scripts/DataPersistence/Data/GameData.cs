using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class GameData 
{
    public ExpirienceData ExpirienceData;
    public int Money;
    public int Health;
    public string SceneName;
    public Dictionary<string, int> QuestIdToStateMap;
    public Dictionary<string, int> InventoryItemIdToAmountmap;
    public List<string> PickedUpItemIds;
    public bool IsNewGame;

    public GameData() {
        QuestIdToStateMap  = new Dictionary<string, int>();
        InventoryItemIdToAmountmap = new Dictionary<string, int>();
        PickedUpItemIds = new List<string>();
        ExpirienceData = new ExpirienceData(1,0);
        Money = 0;
        Health = 0;
        SceneName = "Level1";
        IsNewGame = true;
    }
}
