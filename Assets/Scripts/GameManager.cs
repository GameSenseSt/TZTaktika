using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PublicFields
    public static GameManager instance; //Singletone
    [HideInInspector]
    public List<GameObject> towerCoins; //Upgrading tower icons
    public int wavesCount,  randomValue; //Waves count, random enemies count
    [HideInInspector]
    public int currentWave, score = 0;
    [HideInInspector]
    public float timeBtwWaves;
    [HideInInspector]
    public bool allEnemiesSpawned = false;
    #endregion

    #region SerializedFields
    [SerializeField]
    private int levelNumber; //Level number for reading from the file
    #endregion

    #region PrivateFields
    private string pathToFileLevelInfo = "Assets/Resources/Levels.ini"; //Path to file
    #endregion

    #region MonoBehaviourCallbacks
    void Awake() //Singletone 
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance == this)
        {
            Destroy(gameObject);
        }
        Initialize();
    }

    void Update()
    {
        WinLevel(); //Checking if we won
    }
    #endregion

    #region PrivateMethods
    private void ReadTimeBtwWaves()
    {
        if (File.Exists(pathToFileLevelInfo)) //If file exists
        {
            //Do some manipulation to get time between waves
            string textInLevelFile = File.ReadAllText(pathToFileLevelInfo);
            string key = "l" + levelNumber + ": ";
            string timeBtwWavesString = "";
            int keyPos = textInLevelFile.IndexOf(key) + key.Length;
            while (textInLevelFile[keyPos] != ';')
            {
                timeBtwWavesString += textInLevelFile[keyPos];
                keyPos++;
            }
            timeBtwWaves = int.Parse(timeBtwWavesString);
        }
        else
        {
            Debug.LogError("Не удалось найти файл по указанному пути: " + pathToFileLevelInfo);
        }
    }
    
    private void WinLevel()
    {
        if (currentWave != wavesCount) return;
        if (!allEnemiesSpawned) return;
        if (PoolManager.instance.AliveEnemies == 0) //If it's last wave and all enemies are dead
        {
            UIManager.instance.ShowWinPanel(); //We won
        }
    }

    private void Initialize() //initialization
    {
        ReadTimeBtwWaves(); //Reading time beween waves
        currentWave = 0;
        towerCoins = new List<GameObject>();
        foreach (GameObject towerCoin in GameObject.FindGameObjectsWithTag("TowerCoin"))
        {
            towerCoins.Add(towerCoin);
            towerCoin.SetActive(false);
        }
    }
    #endregion
}
