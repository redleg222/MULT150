using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_zone : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject tank;
    public GameObject housesPrefab;
    public GameObject obstacleTank;
    public GameObject obstacleWire;
    public GameObject powerupHealth;
    public GameObject powerupAmmo;
    public GameObject powerupFuel;
    private bool spawnedObstacles = false;
    private int powerUp = 100;

    void start()
    {
        powerUp = gameManager.GetComponent<game_manager>().houseCount() - Random.Range(12, 25);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.parent.gameObject);
    }

    void Update()
    {
        float obstacles, pos, newpos;
        GameObject temp;
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("house");

        if (gameManager.GetComponent<game_manager>().houseCount() == 0)
        {
            gameObjects = GameObject.FindGameObjectsWithTag("FoW");
            if (gameObjects.Length > 0)
            {
                foreach (GameObject go in gameObjects)
                {
                    Destroy(go);
                }
            }
        }
        else
        { 
            if (gameObjects.Length < 12 )
            {
                Instantiate(housesPrefab, housesPrefab.transform.position, housesPrefab.transform.rotation);

                gameManager.GetComponent<game_manager>().lowerHousecount();

                if (spawnedObstacles)
                {   
                    spawnedObstacles = !spawnedObstacles;
                    //if (gameManager.GetComponent<game_manager>().houseCount() <= powerUp)
                    //{
                    //    powerUp = powerUp - Random.Range(7, 15);
                    //    int i = Random.Range(0, 3);
                    //    if (i == 0)
                    //    {
                    //        temp = Instantiate(powerupFuel) as GameObject;
                    //        temp.GetComponent<trigger_powerup>().gameManager = gameManager;
                    //        Vector3 position = temp.transform.position;
                    //        position.x = Random.Range(-1, 2) * 6;
                    //        temp.transform.position = position;
                    //    }
                    //    else if (i == 1)
                    //    {
                    //        temp = Instantiate(powerupHealth) as GameObject;
                    //        temp.GetComponent<trigger_powerup>().gameManager = gameManager;
                    //        Vector3 position = temp.transform.position;
                    //        position.x = Random.Range(-1, 2) * 6;
                    //        temp.transform.position = position;
                    //    }
                    //    else if (i == 2)
                    //    {
                    //        temp = Instantiate(powerupAmmo) as GameObject;
                    //        temp.GetComponent<trigger_powerup>().gameManager = gameManager;
                    //        Vector3 position = temp.transform.position;
                    //        position.x = Random.Range(-1, 2) * 6;
                    //        temp.transform.position = position;
                    //    }
                    //}
                }
                else
                {
                    spawnedObstacles = !spawnedObstacles;

                    obstacles = Random.Range(0, 3);
                    pos = -1;

                    for (int i = 0; i < obstacles; i++)
                    {

                        newpos = Random.Range(-1, 2) * 6;
                        while (pos == newpos)
                        {
                            newpos = Random.Range(-1, 2) * 6;
                        }
                        pos = newpos;

                        if (Random.Range(0, 2) == 0)
                        {
                            temp = Instantiate(obstacleTank) as GameObject;
                            GameObject child = temp.transform.GetChild(2).GetComponent<trigger_obstacles>().tank = tank;
                            Vector3 position = temp.transform.position;
                            position.x = pos;
                            temp.transform.position = position;
                        }
                        else
                        {
                            temp = Instantiate(obstacleWire) as GameObject;
                            GameObject child = temp.transform.GetChild(5).GetComponent<trigger_obstacles>().tank = tank;
                            Vector3 position = temp.transform.position;
                            position.x = pos;
                            temp.transform.position = position;
                        }
                    }
                }

            }
        }
    }
}
