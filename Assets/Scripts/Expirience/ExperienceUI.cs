using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class ExperienceUI : MonoBehaviour, IDataLoader
{
    [SerializeField] private TextMeshProUGUI _levelTMP;
    [SerializeField] private Image _experienceBar;
    [SerializeField] private float _timeModifierPerXPPoint = 0.5f;

    private int _maxExperience;
    private float _currentExperience;
    private int _currentLevel;
    private ExperienceGetter _experienceGetter;

    [Inject]
    private void Construct(ExperienceGetter experienceGetter)
    {
        _experienceGetter = experienceGetter;
        _experienceGetter.OnExperienceGained += _experienceGetter_OnExperienceGained;
    }

    private void Start()
    {
        _maxExperience = _experienceGetter.GetMaxXPForLevel(_currentLevel);
        UpdateVisual();
    }

    private void _experienceGetter_OnExperienceGained(int updatedLevel, int updatedCurrentXP)
    {
        StartCoroutine(UpdateVisual(updatedLevel, updatedCurrentXP));
    }

    public void UpdateVisual()
    {
        FillExpirienceBar();
        _levelTMP.text = _currentLevel.ToString();
    }

    private void LevelUp()
    {
        _currentLevel++;
        _currentExperience = 0;
        _maxExperience = _experienceGetter.GetMaxXPForLevel(_currentLevel);
        UpdateVisual();
    }

    private void FillExpirienceBar()
    {
        float NormalizedExp = GetNormalizedExperience(_currentExperience);
        _experienceBar.fillAmount = NormalizedExp;
    }

    private float GetNormalizedExperience(float experience)
    {
        return experience / _maxExperience;
    }

    private IEnumerator FillExpirienceCoroutine(int targetExperience)
    {
        var t = 0f;
        float start = _currentExperience;
        float target = targetExperience;
        while (t < 1)
        {
            t += Time.deltaTime / _timeModifierPerXPPoint;

            if (t > 1) t = 1;

            _currentExperience = Mathf.Lerp(start, target, t);
            FillExpirienceBar();

            yield return null;
        }
    }

    private IEnumerator UpdateVisual(int updatedLevel, int updatedExpirience)
    {
        while (_currentLevel < updatedLevel)
        {
            yield return FillExpirienceCoroutine(_maxExperience);
            LevelUp();
        }
        yield return FillExpirienceCoroutine(updatedExpirience);
    }

    public void LoadData(GameData gameData)
    {
        _currentLevel = gameData.ExpirienceData.CurrentLevel;
        _currentExperience = gameData.ExpirienceData.CurrentXpPoints;
    }
}
