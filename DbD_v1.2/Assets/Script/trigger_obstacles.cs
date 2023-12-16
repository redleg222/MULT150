using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_obstacles : MonoBehaviour
{
    [SerializeField] public GameObject gameManager, parent;
    public int obstacleType;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<game_manager>().hitObstacle(obstacleType);
        }
        else if (other.tag != "obstacle")
        {
            Destroy(parent);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<game_manager>().hitObstacle(obstacleType);
        }
    }

}