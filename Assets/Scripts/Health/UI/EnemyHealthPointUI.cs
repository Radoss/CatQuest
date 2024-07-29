using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthPointUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthBarImage;

    private void Start()
    {
        _health.OnHealthChangedEvent += Health_OnHealthChangedEvent;
    }

    private void OnDestroy()
    {
        _health.OnHealthChangedEvent -= Health_OnHealthChangedEvent;
    }

    private void Health_OnHealthChangedEvent()
    {
        _healthBarImage.fillAmount = _health.GetHealthNormalized();
    }
}
