using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Health _health;
    private float _timeToSpawn = 0;
    [SerializeField] private int _deltaTimeToSpawn = 10;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeathEvent += Health_OnDeathEvent;

    }

    private void Health_OnDeathEvent()
    {
        StartCoroutine (StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        while(_timeToSpawn < _deltaTimeToSpawn)
        {
            _timeToSpawn += Time.deltaTime;
            yield return null;
        }
        _health.Revive();
        _timeToSpawn = 0;
    }
}
