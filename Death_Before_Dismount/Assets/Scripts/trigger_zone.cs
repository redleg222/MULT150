using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_zone : MonoBehaviour
{
    public GameObject housesPrefab;
    public GameObject obstacleTank;
    public GameObject obstacleWire;
    public GameObject powerupHealth;
    public GameObject powerupAmmo;
    public GameObject powerupFuel;
    private bool spawnedObstacles = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Destroy(other.transform.parent.gameObject);
    }


    void Update()
    {
        float obstacles;
        GameObject temp;
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("house");

        if (gameObjects.Length < 12)
        {
            Instantiate(housesPrefab, housesPrefab.transform.position, housesPrefab.transform.rotation);

            if (spawnedObstacles)
            {
                spawnedObstacles = !spawnedObstacles;
            }
            else
            {

                spawnedObstacles = !spawnedObstacles;


                obstacles = Random.Range(0, 3);

                if (obstacles == 1)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        Debug.Log("spawn_01");
                        temp = Instantiate(obstacleTank) as GameObject;
                        Vector3 position = temp.transform.position;
                        position.x = Random.Range(-1, 2) * 6;
                        temp.transform.position = position;
                    }
                    else
                    {
                        Debug.Log("spawn_02");
                        temp = Instantiate(obstacleWire) as GameObject;
                        Vector3 position = temp.transform.position;
                        position.x = Random.Range(-1, 2) * 6;
                        temp.transform.position = position;
                    }
                }
                else if (obstacles == 2)
                {
                    // i = Random.Range(-1, 2);
                    if (Random.Range(0, 2) == 0)
                    {
                        Debug.Log("spawn_03");
                        temp = Instantiate(obstacleTank) as GameObject;
                        Vector3 position = temp.transform.position;
                        position.x = Random.Range(-1, 2) * 6;
                        temp.transform.position = position;
                    }
                    else
                    {
                        Debug.Log("spawn_04");
                        temp = Instantiate(obstacleWire) as GameObject;
                        Vector3 position = temp.transform.position;
                        position.x = Random.Range(-1, 2) * 6;
                        temp.transform.position = position;
                    }
                }

            }

        }
    }




}
