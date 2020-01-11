using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    #region SerializedFields
    [SerializeField]
    private int damage; //Tower damage
    [SerializeField]
    private float shotsPerSecond; //Shooting speed
    #endregion

    #region PrivateFields
    private List<Enemy> enemiesInRange; //List of enemies under attack
    private float timeBtwShoot, lastShootTime = 0; //For timer manipulation
    private ParticleSystem shootEffect; //Shooting effect
    #endregion

    #region MonoBehaviourCallbacks
    void Start()
    {
        Initialize(); //Initializtion
    }

    void Update()
    {
        Shooting(); //Shooting
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemiesInRange.Count == 0)
            {
                OnFirstEnemyEntered();
            }
            OnEnemyEntered();
            enemiesInRange.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject.GetComponent<Enemy>());
            if (enemiesInRange.Count == 0)
            {
                OnLastEnemyExited();
            }
            OnEnemyExited();
        }
    }
    #endregion

    #region PublicMethods
    public void UpgradeTower()
    {
        if (Player.instance.GetCoinsCount() >= 100)
        {           
            Player.instance.SpendCoins(100);
            damage++;
        }
    }
    #endregion

    #region PrivateMethods
    private void Initialize()
    {
        enemiesInRange = new List<Enemy>(); //initialization
        timeBtwShoot = 1f / shotsPerSecond; //just more comfortable
        shootEffect = transform.Find("ShootEffect").gameObject.GetComponent<ParticleSystem>();
    }

    private void Shooting()
    {
        if (enemiesInRange.Count == 0) return; //If there is no enemies in range
        if (Time.time > lastShootTime + timeBtwShoot) //If it's time to shoot
        {
            Shoot(enemiesInRange[0]); //Shoot
            lastShootTime = Time.time;
        }
    }

    private void Shoot(Enemy enemy) //Deal damage to enemy
    {
        enemy.GetDamage(damage);
        shootEffect.Play();
    }

    private void OnFirstEnemyEntered() //In case you wanna add some extra code
    {

    }

    private void OnEnemyEntered() //In case you wanna add some extra code 
    {

    }

    private void OnEnemyExited() //In case you wanna add some extra code 
    {

    }

    private void OnLastEnemyExited() //In case you wanna add some extra code 
    {

    }
    #endregion
}
