using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region PublicFields
    public static PoolManager instance; //Singletone
    public GameObject EnemyPrefab; //Enemy prefab
    public int AliveEnemies = 0; //Count of alive enemies right now
    #endregion

    #region SerializedFields
    [SerializeField]
    private int enemiesCount; //pPool size
    #endregion

    #region PrivateFields
    private Stack<GameObject> enemies; //Pool
    private Transform spawnPoint; //Spawn point
    #endregion

    #region MonoBehaviourCallbacks
    private void Awake()//Singletone
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize(); //Initializations
    }
    #endregion

    #region PublicMethods
    public void Create() //Creating enemy
    {
        GameObject tempEnemy = enemies.Pop(); //Getting enemy from the pool
        tempEnemy.transform.position = spawnPoint.position; //Placin enemy
        tempEnemy.SetActive(true); //Making him active
        tempEnemy.GetComponent<Enemy>().ResetEnemy(); //Reset him
        tempEnemy.GetComponent<Enemy>().GetDamage(-GameManager.instance.currentWave); //Making him stronger
        tempEnemy.GetComponent<Enemy>().SetNewMaxHealth(); //Again
        AliveEnemies++; //Increase count of alive enemies
    }

    public void Destroy(GameObject enemy) //Destroying enemy
    {
        enemy.transform.position = Vector3.zero; //Place him back
        enemies.Push(enemy); //Push him into pool
        enemy.SetActive(false); //Deactivating him
        AliveEnemies--; //Decrease amount of alive enemies
    }
    #endregion

    #region PrivateMethods
    private void Initialize() //Initialization
    {
        GameObject tempEnemy; //Temp gameobject
        spawnPoint = GameObject.Find("StartPoint").transform; //Where should we spawn
        enemies = new Stack<GameObject>(); //Initialization
        for (int z=0; z<enemiesCount; z++) //Creating enemies
        {           
            tempEnemy = Instantiate(EnemyPrefab, transform);
            tempEnemy.name = "Enemy" + z;
            enemies.Push(tempEnemy);
            tempEnemy.SetActive(false);
        }
    }
    #endregion

}
