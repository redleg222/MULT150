using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float speed = 0.05f;
    public GameObject tank;
    public GameObject Ltrack;
    public GameObject Lwheel;
    public GameObject Rtrack;
    public GameObject Rwheel;
    public static bool moveForward = true;
    public static bool moveBackward = true;
    private Renderer rend;
    private Renderer rendL;
    private Renderer rendR;
    public ProgressBar pbFuel, pbHealth, pbAmmo;
    private GameObject[] houses;
    private GameObject[] powerups;
    private GameObject[] obstacles;


    public void AdjustTime(float amount)
    {
        Debug.Log("Boo!");
        //gameTime += amount;
        //if (amount < 0) SlowWorldDown();
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rendL = Ltrack.GetComponent<Renderer>();
        rendR = Rtrack.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed * -1;
        rend.material.mainTextureOffset = new Vector2(0, offset);
        rendL.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        rendR.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        houses = GameObject.FindGameObjectsWithTag("house");
        powerups = GameObject.FindGameObjectsWithTag("powerup");
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        foreach (GameObject house in houses)
        {
            house.transform.position += Vector3.forward * speed * -12 * Time.deltaTime;
        }

        foreach (GameObject powerup in powerups)
        {
            powerup.transform.position += Vector3.forward * speed * -12 * Time.deltaTime;
        }

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.position += Vector3.forward * speed * -12 * Time.deltaTime;
        }

        for (int i = 0; i < Lwheel.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = Lwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = speed / wheelSize;

            //Apply rotation
            Lwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }

        for (int i = 0; i < Rwheel.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = Rwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = speed / wheelSize;

            //Apply rotation
            Rwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }
    }
        //public TextureScroller ground;
        //public float gameTime = 10f;

        //private float totalTime = 0f;
        //private bool gameOver = false;

        //void Update()
        //{
        //    if (!gameOver)
        //   {
        //        totalTime += Time.deltaTime;
        //        gameTime -= Time.deltaTime;
        //        gameOver = gameTime <= 0;
        //    }
        //}

        //public void AdjustTime(float amount)
        //{
        //    gameTime += amount;
        //    if (amount < 0) SlowWorldDown();
        // }

        // void SlowWorldDown()
        //{
        //    CancelInvoke();
        //     Time.timeScale = 0.5f;
        //     Invoke("SpeedWorldUp", 1);
        // }

        // void SpeedWorldUp()
        // {
        //      Time.timeScale = 1f;
        //  }

        // void OnGUI()
        // {
        //     if (!gameOver)
        //     {
        //         Rect rect1 = new Rect(Screen.width / 2 - 80, 20, 160, 28);
        //         GUI.Box(rect1, "Time Remaining: " + Mathf.Round(gameTime));
        //     }
        //     else
        //     {
        //         Rect rect2 = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 54);
        //         GUI.Box(rect2, "Game Over");
        //          Rect rect3 = new Rect(Screen.width / 2 - 30, Screen.height / 2 - 25, 60, 50);
        //          GUI.Label(rect3, "Good Job!");
        //          Rect rect1 = new Rect(Screen.width / 2 - 80, 20, 160, 28);
        //          GUI.Box(rect1, "Total Time: " + Mathf.Round(totalTime));
        //      }
        //   }

    }
