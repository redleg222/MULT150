using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    [SerializeField] private GameObject GameManager, tank;

    void Update()
    {
        if (GameManager.GetComponent<GameManager>().finalApproach)
        {
            transform.position = new Vector3(0, 5, tank.transform.position.z - 12);
        }
    }


}
