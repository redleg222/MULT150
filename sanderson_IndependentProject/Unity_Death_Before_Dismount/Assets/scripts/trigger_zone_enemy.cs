using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_zone_enemy : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            Destroy(other.transform.gameObject);
        }
    }
}
