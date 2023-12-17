using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trigger_canvas : MonoBehaviour
{
    #region Variables
    [Header("Game Manager")]
    public GameObject GameManager;

    [Header("cards")]
    public GameObject gameStart;
    public GameObject gameWon;
    public GameObject gameLost;
    public GameObject buttonReplay;
    #endregion

    public void beginGame()
    {
        GameManager.GetComponent<GameManager>().tank.SetActive(true);
        gameStart.gameObject.SetActive(false);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
