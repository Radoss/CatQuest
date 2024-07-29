using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour, IDataLoader
{
    [SerializeField] DataPersistenceManager dataPersistenceManager;
    [SerializeField] Button _loadButton;
    private string sceneToLoad;

    public void LoadData(GameData gameData)
    {
        if(gameData.IsNewGame)
        {
            _loadButton.interactable = false;
        }
        sceneToLoad = gameData.SceneName;
    }

    public void LoadLevelBtnClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void NewGameBtnClicked()
    {
        dataPersistenceManager.StartNewGame();
    }
}
