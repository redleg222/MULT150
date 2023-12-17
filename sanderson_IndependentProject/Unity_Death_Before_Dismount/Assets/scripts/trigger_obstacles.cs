using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_obstacles : MonoBehaviour
{
    #region Variables
    [Header("Assets")]
    public GameObject GameManager;
    public GameObject parent;

    [Header("Obstacle ID")]
    public int obstacleType;
    #endregion

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
