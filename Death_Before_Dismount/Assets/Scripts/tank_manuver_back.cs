using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_manuver_back : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.moveBackward = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.moveBackward = true;
        }
    }
}
