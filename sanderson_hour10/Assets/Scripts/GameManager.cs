using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GoalScript blue, green, red, orange;
    private bool isGameOver = true;
    private float gamestart;
    private float gameend;
    
    void Start()
    {
        gamestart = Time.time;
    }



    // Update is called once per frame
    void Update()
    {
        isGameOver = blue.isSolved && green.isSolved && red.isSolved && orange.isSolved;
        if (isGameOver && gameend == 0)
        {
            gameend = Time.time;
        }
    }

    void OnGUI()
    {
        if (isGameOver) 
        { Rect rect2 = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 54);
            GUI.Box(rect2, "Game Over");
            Rect rect3 = new Rect(Screen.width / 2 - 30, Screen.height / 2 - 25, 60, 50);
            GUI.Label(rect3, "Good Job!");
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 10, 80, 34), "Play Again?"))
                Application.LoadLevel(0);
            Rect rect1 = new Rect(Screen.width / 2 - 50, 20, 100, 28);
            GUI.Box(rect1, "Seconds: " + Mathf.Round(gameend - gamestart));
        } else
        {
            Rect rect1 = new Rect(Screen.width / 2 - 50, 20, 100, 28);
            GUI.Box(rect1, "Seconds: " + Mathf.Round(Time.time - gamestart));
        }
            }
}
