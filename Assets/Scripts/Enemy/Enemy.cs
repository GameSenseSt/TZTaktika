using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private int health, damage, goldForKill, speed; //Enemy params
    [SerializeField]
    private float timeBtwShoot, lastShoot; //shooting params
    [SerializeField]
    private Transform healthLine;   //Health bar
    #endregion

    #region PrivateFields
    private EnemyWayPointsController wayPointsController; //For getting way points
    private Transform pointNow; //Point that is current enemies target
    private int indexOfPointNow = 0, curHealth, healthOnSpawn; //For duture health manipilation
    private bool finished = false; //Does enemy reached player's base
    #endregion

    #region MonoBehaviourCallbacks
    private void Start()
    {
        Initialize(); //Initialization
        OnCreated(); //In case you want to add something else after enemy creation
    }

    private void Update()
    {
        if (!finished) //If player's base isn't reached
        {
            Move(); //Move next
        } else
        {
            Shooting(); //Else destroy player's base
        }
    }
    #endregion

    #region PublicMethods
    public void ResetEnemy() //Whenever enemy spawned he should be refresshed
    {
        curHealth = health; //Set current hp to current hp
        indexOfPointNow = 0; //Enemy goes to the 0 point again
        finished = false; //Enemy is far away from player's base
        pointNow = EnemyWayPointsHolder.WayPoints[0]; //Go to 0 point
        healthLine.transform.localScale = Vector3.one; //Reset health bar
    }

    public void SetNewMaxHealth() //With new waves enemies become stronger
    {
        healthOnSpawn = curHealth; //Update max health
    }
    
    public void GetDamage(int damage) //Getting damage
    {
        curHealth -= damage; //Substract the damage from current hp
        if (damage<0) //The only way to get negative damage is refreshing enemy
        {
            SetNewMaxHealth(); //Update enemies max health
        }
        
        healthLine.localScale = new Vector3(1f * curHealth / healthOnSpawn, 1, 1); //Change health bar
        if (curHealth<=0) //If enemy is dead
        {
            OnDestroyed(); //In case you wanna do something when enemy is died
            PoolManager.instance.Destroy(gameObject); //Bring enemy back to pool
        }
    }
    #endregion

    #region PrivateMethods
    private void Initialize() //Just initialization
    {
        ResetEnemy(); //Reset
        pointNow = EnemyWayPointsHolder.WayPoints[0]; //Going to 0 point
    }

    private void Move() //Movement
    {
        //Moving to current target
        transform.position = Vector2.MoveTowards(transform.position, pointNow.position, speed * Time.deltaTime);
        LookingForward(); //looking at the target
        if (transform.position == pointNow.position) //if enemy reached the target
        {
            OnReachedWayPoint(); //in case you wanna write extra code
            if (pointNow.CompareTag("Finish")) //if enemy reached player's base
            {
                finished = true; //change the flag               
                OnReachedDestination(); //in case you wanna add extra code
                return;
            }           
            pointNow = EnemyWayPointsHolder.WayPoints[indexOfPointNow]; //going to the next point
        }
    }

    private void LookingForward() //Just looking at the target
    {
        Vector3 direction = pointNow.position - transform.position; //Vector from enemy to target
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void Shooting() //Shooting 
    {
        if (Time.time > lastShoot + timeBtwShoot) //If "reloading" is finished
        {
            Player.instance.GetDamage(damage); //Player takes damage
            lastShoot = Time.time; //Refresh last shooting time
        }
    }

    private void OnCreated() //Just for flexibility 
    {

    }

    private void OnGotDamage() //Just for flexibility 
    {

    }

    private void OnDealtDamage() //Just for flexibility 
    {

    }

    private void OnReachedWayPoint() //Just for flexibility
    {
        indexOfPointNow++;
    } 

    private void OnReachedDestination() //Just for flexibility + My code inside
    {
        //Looking at player's base when came
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.Find("ShootingEffect").gameObject.SetActive(true);
    }

    private void OnDestroyed()//Just for flexibility + My code inside
    {
        Player.instance.GetCoins(goldForKill); //Give gold to player
        GameManager.instance.score++; //Update our score
    }
    #endregion

}
