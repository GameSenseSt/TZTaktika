using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTowerButton : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private int towerCost;
    #endregion

    #region MonoBehaviourCallbacks
    private void Start()
    {
        Initialize();
    }
    #endregion

    #region PublicMethods
    public void OnClick()
    {
        Instantiate(tower, Input.mousePosition, Quaternion.identity);
        Player.instance.SpendCoins(towerCost);
    }
    #endregion

    #region PrivateMethods
    private void Initialize()
    {
        transform.Find("CostText").gameObject.GetComponent<Text>().text = towerCost.ToString();
    }
    #endregion
}
