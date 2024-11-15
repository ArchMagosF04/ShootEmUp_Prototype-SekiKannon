using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameaData;

    private List<IDataPersistance> dataPersistances;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        dataPersistances = FindAllDataPersistanceObjects();
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    public void NewGame()
    {
        this.gameaData = new GameData(BulletColorManager.Instance.SquareColorDefault, BulletColorManager.Instance.CircleColorDefault, BulletColorManager.Instance.RonboidColorDefault, BulletColorManager.Instance.TriangleColorDefault);
    }

    public void LoadGame()
    {
        this.gameaData = dataHandler.Load();

        if (this.gameaData == null)
        {
            Debug.Log("No data was Found. Initializing data to defaults");
            NewGame();
        }

        foreach (IDataPersistance persistance in dataPersistances)
        {
            persistance.LoadData(gameaData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistance persistance in dataPersistances)
        {
            persistance.SaveData(ref gameaData);
        }

        dataHandler.Save(gameaData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
