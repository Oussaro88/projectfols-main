using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

//[CustomEditor(typeof(DataPersistenceManager))]
//class ButtonEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        DataPersistenceManager dataPersistenceManager = (DataPersistenceManager)target;
//        if (GUILayout.Button("Delete Save File"))
//        {
//            dataPersistenceManager.DeleteFile();
//        }
//    }
//}

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }
    public GameData GameData { get => gameData; set => gameData = value; }
    public FileDataHandler DataHandler { get => dataHandler; set => dataHandler = value; }

    public bool newSceneLoading;
    public bool unlockedLevel;
    public bool gateOpened;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        this.DataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded Called...");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public void NewGame()
    {
        this.GameData = new GameData();
    }

    public void LoadGame()
    {
        this.GameData = DataHandler.Load();

        if(this.GameData == null)
        {
            Debug.Log("No data found. A New Game needs to be started before data can be loaded.");
            NewGame();
        }

        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            unlockedLevel = this.GameData.unlockedNewLevel;
            
            foreach (IDataPersistence dataPersistenceObjs in dataPersistenceObjects)
            {
                dataPersistenceObjs.LoadData(GameData);
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            gateOpened = this.GameData.bossOpen;
        }
    }

    public void SaveGame()
    {
        if(this.GameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
        }

        foreach (IDataPersistence dataPersistenceObjs in dataPersistenceObjects)
        {
            dataPersistenceObjs.SaveData(GameData);
        }

        this.GameData.unlockedNewLevel = unlockedLevel;
        this.GameData.bossOpen = gateOpened;
        DataHandler.Save(GameData);
        Debug.Log("Game Saved...");
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public void DeleteFile()
    {
        if(File.Exists(Application.persistentDataPath + "/data.game"))
        {
            File.Delete(Application.persistentDataPath + "/data.game");
            Debug.Log("Save File of the game has been deleted.");
        }
        else
        {
            Debug.Log("No Save File was found to be deleted.");
        }
    }
}
