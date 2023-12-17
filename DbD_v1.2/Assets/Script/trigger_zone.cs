using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_zone : MonoBehaviour
{
    [Header("Game Manager")]
    public GameObject GameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            Destroy(other.transform.gameObject);
        }
        else if (other.transform.parent.gameObject.name == "smokeScreen(Clone)")
        {
            GameManager.GetComponent<GameManager>().smokeCover = false;
        }
            Destroy(other.transform.parent.gameObject);
    }

}
