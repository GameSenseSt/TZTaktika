using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private int health, money; //Health and money, yeah
    #endregion

    #region PublicFields
    public static Player instance; //Singletone
    #endregion

    #region PrivateFields
    private bool isAlive = true; //if player alive
    #endregion

    #region MonoBehaviourCallbacks
    private void Awake() //Singletone 
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Initialize(); //Initialization
    }

    #endregion

    #region PublicMethods
    public void GetDamage(int damage) //Getting damage
    {
        if (!isAlive) return;
        health -= damage;
        UIManager.instance.SetHealthUI(health); //Update interface
        if (health <= 0) //if player is dead
        {
            isAlive = false; //He's not alive, true story
            OnDefeated(); //In case you wanna add some extra code
        }
    }
    
    public void GetCoins(int count) //Add coins to the player
    {
        money += count;
        UIManager.instance.SetCoins(money);
        if (money >=100 && GameManager.instance.towerCoins.Count>0) //If player can upgrade tower
        {
            if (GameManager.instance.towerCoins[0].activeSelf) return;
            foreach (GameObject towerCoin in GameManager.instance.towerCoins)
            {
                towerCoin.SetActive(true); //Activating upgrade buttons
            }
            
        }
    }

    public void SpendCoins(int count) //Wasting the money
    {
        money -= count;
        UIManager.instance.SetCoins(money);
        if (money < 100 && GameManager.instance.towerCoins.Count > 0)
        {
            if (!GameManager.instance.towerCoins[0].activeSelf) return;
            foreach (GameObject towerCoin in GameManager.instance.towerCoins)
            {
                towerCoin.SetActive(false);
            }

        }
    }

    public int GetCoinsCount()
    {
        return money;
    }
    #endregion

    #region PrivateMethods
    private void Initialize()
    {
        UIManager.instance.SetHealthUI(health); //Set health UI
        UIManager.instance.SetCoins(money); //Set start money UI
    }

    private void OnGotDamage() //In case you wanna add some extra code
    {

    }

    private void OnEnemyDestroyed() //In case you wanna add some extra code
    {

    }

    private void OnWaveCompleted() //In case you wanna add some extra code
    {

    }
    
    private void OnDefeated()
    {
        UIManager.instance.ShowDefeatPanel(); //Show this sad panel
    }
    #endregion
}
