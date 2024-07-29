using UnityEngine;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField] private GameObject _fullHeartGO;
    [SerializeField] private GameObject _emptyHeartGO;

    public void SetActive(bool isActive)
    {
        _fullHeartGO.SetActive(isActive);
        _emptyHeartGO.SetActive(!isActive);
    }
}
