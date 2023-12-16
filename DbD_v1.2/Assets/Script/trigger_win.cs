using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_win : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<game_manager>().finish();
        }
    }
}
