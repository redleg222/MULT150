using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_manuver_front : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.moveForward = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.moveForward = true;
        }
    }

}
