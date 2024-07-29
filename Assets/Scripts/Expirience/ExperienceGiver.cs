using System;
using UnityEngine;

public class ExperienceGiver : MonoBehaviour
{
    [SerializeField] private int _numberOfGivenExperience;

    public static event Action<int> OnExperienceGiven;

    public void GiveExpirience(int numberOfGivenExperience)
    {
        OnExperienceGiven?.Invoke(numberOfGivenExperience);
    }

    public void GiveExpirience()
    {
        OnExperienceGiven?.Invoke(_numberOfGivenExperience);
    }
}
