using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public void newGame()
    {
        SceneManager.LoadScene("level_one");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
