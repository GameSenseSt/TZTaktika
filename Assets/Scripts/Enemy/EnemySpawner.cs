using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private float timeBtwSpawn; //Time between spawn
    #endregion

    #region PrivateFields
    //Time between waves, last spawned time, ant time< when last wave have finished
    private float timeBtwWaves, lastSpawned, lastWaveFinishedAt = 0;
    private int enemiesThisWave; //Count of enemies that will be spawned this wave
    //Wave is is still going on, all enemies has been spawned
    private bool waveIsGoingOn = false, allEnemiesSpawned = false;
    #endregion

    #region MonoBehaviourCallback
    void Start()
    {
        Initialize(); //Initialization
        StartNewWave(); //starting first wave
    }

    
    void Update()
    {
        SpawningProcess(); //Spawning Process
        WaitingForNextWave(); //If spawning process of the wave is finished, waiting for the next wave
    }
    #endregion

    #region PrivateMethods
    private void Initialize() //Initialization
    {
        timeBtwWaves = GameManager.instance.timeBtwWaves; //Getting time between waves from file
    }

    private void SpawningProcess()
    {
        if (allEnemiesSpawned) return; //If evetyone are spawned
        if (!waveIsGoingOn) return; //If new wave didn't start
        if (Time.time > lastSpawned + timeBtwSpawn) //If it's time to spawn new enemy
        {
            SpawnEnemy(); //Spawn enemy
            lastSpawned = Time.time; //Update last spawned time
            enemiesThisWave--; //Decrease count of enemies that will be spawned this wave
            if (enemiesThisWave==0) //If all enemies are spawned
            {
                waveIsGoingOn = false; //Wave is finished
                OnWaveFinished(); //In case you wanna add some extra code
                lastWaveFinishedAt = Time.time; //When wave was finished
                if (GameManager.instance.currentWave == GameManager.instance.wavesCount) //if it was the last wave
                {
                    allEnemiesSpawned = true; //All enemies are spawned
                    GameManager.instance.allEnemiesSpawned = true; //Telling that to GameManger
                }
            }
        }
    }

    private void WaitingForNextWave() //Just timer to the next wave
    {
        if (allEnemiesSpawned) return;
        if (waveIsGoingOn) return;
        if (Time.time > lastWaveFinishedAt + timeBtwWaves)
        {
            waveIsGoingOn = false;
            StartNewWave();            
        }

    }

    private void SpawnEnemy()
    {
        PoolManager.instance.Create(); //Taking enemy from pool
    }

    private void CalculateEnemiesCountThisWave() //Getting random count of enemies
    {
        int minRandomRange = GameManager.instance.currentWave;
        int maxRandomRange = minRandomRange + GameManager.instance.randomValue + 1;
        enemiesThisWave = Random.Range(minRandomRange, maxRandomRange);
    }

    private void StartNewWave()
    {
        GameManager.instance.currentWave++; //Increase current wave number
        CalculateEnemiesCountThisWave(); //Calculating enemies count
        waveIsGoingOn = true; //Wave is started
        OnWaveStarted(); //In case you wanna add some extra code
        UIManager.instance.WaveText("Wave " + GameManager.instance.currentWave); //Message to the player
    }

    private void OnWaveFinished() //Just for extra code
    {

    }

    private void OnWaveStarted() //Just for extra code
    {

    }
    #endregion
}
