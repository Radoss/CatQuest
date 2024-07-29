using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager: MonoBehaviour, INewGameStarter
{
    [SerializeField] private string _fileName;

    private bool _useEncryption = true;
    private FileDataHandler _fileHandler;
    private GameData _gameData;
    private List<IDataSaver> _objectsToSaveList;
    private List<IDataLoader> _objectsToLoadList;

    private void Awake()
    {
        _fileHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
        FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void FindAllDataPersistenceObjects()
    {
        MonoBehaviour[] monoBehaviourArray = Object.FindObjectsOfType<MonoBehaviour>();

        IEnumerable<IDataSaver> objectsToSaveEnum = monoBehaviourArray.OfType<IDataSaver>();
        _objectsToSaveList = new List<IDataSaver>(objectsToSaveEnum);

        IEnumerable<IDataLoader> objectsToLoadEnum = monoBehaviourArray.OfType<IDataLoader>();
        _objectsToLoadList = new List<IDataLoader>(objectsToLoadEnum);
    }

    private void InitNewGameData() {
        _gameData = new GameData();
    }

    private void LoadGame()
    {
        _gameData = _fileHandler.Load();
        if (_gameData == null )
        {
            InitNewGameData();
        }
        foreach (IDataLoader dataPersistenceObject in _objectsToLoadList)
        {
            dataPersistenceObject.LoadData(_gameData);
        }
    }

    private void OnDisable()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        if (_gameData == null )
        {
            return;
        }

        _gameData.IsNewGame = false;

        foreach (IDataSaver dataPersistenceObject in _objectsToSaveList)
        {
            dataPersistenceObject.SaveData(_gameData);
        }
        _gameData.SceneName = SceneManager.GetActiveScene().name;
        _fileHandler.Save(_gameData);
    }

    private void DeleteGameData()
    {

    }

    public void StartNewGame()
    {
        _fileHandler.DeleteSaveFile();
        _gameData = null;
        SceneManager.LoadScene("Level1");
    }
}
