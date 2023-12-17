using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_powerup : MonoBehaviour
{
    #region Variables
    [Header("Assets")]
    public GameObject GameManager;
    public GameObject parent;

    [Header("Obstacle ID")]
    public int powerupType;
    #endregion

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
