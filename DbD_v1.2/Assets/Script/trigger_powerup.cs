using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_powerup : MonoBehaviour
{
    [SerializeField] public GameObject gameManager, parent;
    public int powerupType;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<game_manager>().powerup(powerupType);
            Destroy(parent);
        }
        else if (other.tag != "Untagged")
        {
            Destroy(parent);
        }


    }

}
