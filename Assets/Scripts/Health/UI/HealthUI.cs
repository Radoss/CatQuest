using UnityEngine;
using Zenject;

public class HealthUI : MonoBehaviour, IDataLoader
{
    [SerializeField] private GameObject _healthPointPrefab;
    private HealthPointUI[] _healthPoints;
    private IHealth _playerHealth;

    [Inject]
    private void Construct(IHealth playerHealth)
    {
        _playerHealth = playerHealth;
        GenerateHealthPointsUI();
    }
    private void Start()
    {
        _playerHealth.OnHealthChangedEvent += PlayerHealth_OnHealthChangedEvent;
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthChangedEvent -= PlayerHealth_OnHealthChangedEvent;
    }

    private void GenerateHealthPointsUI()
    {
        _healthPoints = new HealthPointUI[_playerHealth.MaxHealth];
        for (int i = 0; i < _playerHealth.MaxHealth; i++)
        {
            GameObject healthPoint = Instantiate(_healthPointPrefab, transform);
            _healthPoints[i] = healthPoint.GetComponent<HealthPointUI>();
        }
    }

    private void PlayerHealth_OnHealthChangedEvent()
    {
        UpdateVisual(_playerHealth.CurrentHealth);
    }

    private void UpdateVisual(int currentHealth)
    {
        for (int i = 0; i < _playerHealth.MaxHealth; i++)
        {
            _healthPoints[i].SetActive(i < currentHealth);
        }
    }

    public void LoadData(GameData gameData)
    {
        int health = gameData.Health > 0 ? gameData.Health : _playerHealth.MaxHealth;
        UpdateVisual(health);
    }
}
