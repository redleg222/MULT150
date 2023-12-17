using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_obstacles : MonoBehaviour
{
    [SerializeField] public GameObject GameManager, parent;
    public int obstacleType;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.GetComponent<GameManager>().hitObstacle(obstacleType);
        }
        else if (other.tag == "enemy")
        {
            //Debug.Log("thought so");
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
            GameManager.GetComponent<GameManager>().hitObstacle(obstacleType);
        }
    }

}
