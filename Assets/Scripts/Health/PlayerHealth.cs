using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health, IDataLoader, IDataSaver
{
    public void LoadData(GameData gameData)
    {
        _currentHealth = gameData.Health > 0 ? gameData.Health : MaxHealth;
    }

    public void SaveData(GameData gameData)
    {
        gameData.Health = _currentHealth;
    }
}
