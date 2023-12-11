using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject powerupPrefab;
    public GameObject obstaclePrefab;
    public float spawnCycle = 0.5f;

    private GameManager manager;
    private float totalTime = 0f;
    private bool spawnPowerup = true;

    void Start()
    {
        manager = GetComponent<GameManager>();
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > spawnCycle)
        {
            GameObject temp;
            if (spawnPowerup)
                temp = Instantiate(powerupPrefab) as GameObject;
            else
                temp = Instantiate(obstaclePrefab) as GameObject;

            Vector3 position = temp.transform.position;
            position.x = Random.Range(-3f, 3f);
            temp.transform.position = position;

            Collidable col = temp.GetComponent<Collidable>();
            col.manager = manager;

            totalTime = 0;
            spawnPowerup = !spawnPowerup;
        }
    }
}
