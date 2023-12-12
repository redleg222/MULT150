using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject tank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.GetComponent<game_manager>().houseCount() == 0)
        {
            transform.position = new Vector3(0, 5, tank.transform.position.z - 12);
        }
    }
}
