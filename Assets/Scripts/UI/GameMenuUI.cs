using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] GameObject _gameMenuPanel;
    private INewGameStarter _newGameStarter;
    private IInput _playerInput;
    private bool IsActive;

    [Inject]
    private void Construct(INewGameStarter newGameStarter, IInput input)
    {
        _newGameStarter = newGameStarter;
        _playerInput = input;
        _playerInput.OnToggleGameMenu += _playerInput_OnToggleGameMenu;
        IsActive = _gameMenuPanel.activeSelf;
    }

    private void _playerInput_OnToggleGameMenu()
    {
        IsActive = !IsActive;
        _gameMenuPanel.SetActive(IsActive);
    }

    public void OnNewGameClicked()
    {
        _newGameStarter.StartNewGame();
    }

    public void OnTryAgainClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
