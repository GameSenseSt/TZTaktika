using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCoin : MonoBehaviour
{
    public void OnTap() //Upgrading the tower
    {
        transform.parent.gameObject.GetComponent<Tower>().UpgradeTower();
    }
}
