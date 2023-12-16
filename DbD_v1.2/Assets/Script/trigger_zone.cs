using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_zone : MonoBehaviour
{
    [Header("Game Manager")]
    public GameObject gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.name == "smokeScreen(Clone)")
        {
            gameManager.GetComponent<game_manager>().SmokeCover = false;
        }
            Destroy(other.transform.parent.gameObject);
    }

}
