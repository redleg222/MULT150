using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextureScroller ground;
    public float gameTime = 10f;

    private float totalTime = 0f;
    private bool gameOver = false;

    void Update()
    {
        if (!gameOver)
        {
            totalTime += Time.deltaTime;
            gameTime -= Time.deltaTime;
            gameOver = gameTime <= 0;
        }
    }

    public void AdjustTime(float amount)
    {
        gameTime += amount;
        if (amount < 0) SlowWorldDown();
    }

    void SlowWorldDown()
    {
        CancelInvoke();
        Time.timeScale = 0.5f;
        Invoke("SpeedWorldUp", 1);
    }

    void SpeedWorldUp()
    {
        Time.timeScale = 1f;
    }

    void OnGUI()
    {
        if (!gameOver)
        {
            Rect rect1 = new Rect(Screen.width / 2 - 80, 20, 160, 28);
            GUI.Box(rect1, "Time Remaining: " + Mathf.Round(gameTime));
        }
        else
        {
            Rect rect2 = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 54);
            GUI.Box(rect2, "Game Over");
            Rect rect3 = new Rect(Screen.width / 2 - 30, Screen.height / 2 - 25, 60, 50);
            GUI.Label(rect3, "Good Job!");
            Rect rect1 = new Rect(Screen.width / 2 - 80, 20, 160, 28);
            GUI.Box(rect1, "Total Time: " + Mathf.Round(totalTime));
        }
    }

}
