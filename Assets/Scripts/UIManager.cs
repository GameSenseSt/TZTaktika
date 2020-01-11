using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    #region PublicFields
    public static UIManager instance; //Singletone
    #endregion

    #region SerializedFields
    [SerializeField]
    private GameObject defeatPanel, waveText, winPanel; //Panels
    [SerializeField]
    private Text heartText, coinText; //Texts
    #endregion

    #region MonoBehaviourCallbacks
    private void Awake() //Singletone again
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
    #endregion

    #region PublicMethods
    public void ShowDefeatPanel()
    {
        defeatPanel.SetActive(true);
        defeatPanel.transform.Find("ScoreText").GetComponent<Text>().text = "Enemies destroyed: " + 
                                                                             GameManager.instance.score.ToString();
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        winPanel.transform.Find("ScoreText").GetComponent<Text>().text = "Enemies destroyed: " +
                                                                             GameManager.instance.score.ToString();
    }
    
    public void SetHealthUI(int health)
    {
        heartText.text = health.ToString();
    }

    public void SetCoins(int coins)
    {
        coinText.text = coins.ToString() + "/100";
    }

    public void WaveText(string text)
    {
        waveText.GetComponent<Text>().text = text;
        waveText.GetComponent<Animator>().SetTrigger("StartNewWave");
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void OnWinClick()
    {
        SceneManager.LoadScene(Application.loadedLevel + 1);
    }
    #endregion
}
