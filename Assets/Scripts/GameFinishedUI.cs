using System;
using UnityEngine;
using Zenject;

public class GameFinishedUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameFinishedPanel;
    [SerializeField] private ParticleSystem _starsParticles;

    [Inject]
    private void Construct(CurrentGame currentGame)
    {
        currentGame.OnGameFinished += CurrentGame_OnGameFinished;
    }

    private void CurrentGame_OnGameFinished()
    {
        _gameFinishedPanel.SetActive(true);
        _starsParticles.Play();
    }
}
