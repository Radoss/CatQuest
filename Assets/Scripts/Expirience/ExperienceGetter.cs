using UnityEngine;
using System;

public class ExperienceGetter : MonoBehaviour, IDataSaver, IDataLoader
{
    public event Action<int, int> OnExperienceGained;

    private int[] _levelToMaxExperience;
    private int _currentLevel = 1;
    private int _currentExperience = 0;
    private int _maxLevel;
    private int _currentMaxExpirience;

    private void Start()
    {
        ExperienceGiver.OnExperienceGiven += ExperienceGiver_OnExperienceGiven;
    }

    private void OnDestroy()
    {
        ExperienceGiver.OnExperienceGiven -= ExperienceGiver_OnExperienceGiven;
    }

    private void ExperienceGiver_OnExperienceGiven(int numberOfExperience)
    {
        AddExperience(numberOfExperience);
        OnExperienceGained?.Invoke(_currentLevel, _currentExperience);
    }

    private void LevelUp()
    {
        _currentExperience = 0;
        _currentLevel++;
    }

    private void AddExperience(int expirience)
    {
        if (_currentLevel == _maxLevel || expirience == 0)
        {
            return;
        }
        int xpToAddToCurrentLevel = Mathf.Clamp(expirience, 0, _currentMaxExpirience - _currentExperience);
        expirience -= xpToAddToCurrentLevel;
        _currentExperience += xpToAddToCurrentLevel;
        if (_currentExperience == _currentMaxExpirience)
        {
            LevelUp();
            _currentMaxExpirience = GetMaxXPForLevel(_currentLevel);
        }
        AddExperience(expirience);
    }

    public float GetNormalizedCurrentXP()
    {
        return (float)_currentExperience / _currentMaxExpirience;
    }

    public int GetMaxXPForLevel(int level)
    {
        return _levelToMaxExperience[level - 1];
    }

    public void LoadData(GameData gameData)
    {
        _currentLevel = gameData.ExpirienceData.CurrentLevel;
        _currentExperience = gameData.ExpirienceData.CurrentXpPoints;
        _levelToMaxExperience = new int[] { 100, 200, 300, 400, 500 };
        _maxLevel = _levelToMaxExperience.Length;
        _currentMaxExpirience = GetMaxXPForLevel(_currentLevel);
    }

    public void SaveData(GameData gameData)
    {
        gameData.ExpirienceData = new ExpirienceData(_currentLevel, _currentExperience);
    }
}
