using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    [SerializeField] private GameObject gameManager, tank;
    [SerializeField] public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<game_manager>().HouseCountdown == 0)
        {
            transform.position = new Vector3(0, 5, tank.transform.position.z - 12);
        }
    }


}
