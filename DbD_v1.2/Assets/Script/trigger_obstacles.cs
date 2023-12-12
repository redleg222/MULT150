using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_obstacles : MonoBehaviour
{
    [SerializeField] public GameObject tank, parent;
    public int obstacleType;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tank.GetComponent<shermanTank>().obstacle(obstacleType);
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
            tank.GetComponent<shermanTank>().obstacle(obstacleType);
        }
    }



}
