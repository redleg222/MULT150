using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_powerup : MonoBehaviour
{
    [SerializeField] public GameObject GameManager, parent;
    public int powerupType;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.GetComponent<GameManager>().powerup(powerupType);
            Destroy(parent);
        }
        else if (other.tag == "enemy")
        {
            Destroy(parent);
        }
        else if (other.tag != "Untagged")
        {
            Destroy(parent);
        }


    }

}
