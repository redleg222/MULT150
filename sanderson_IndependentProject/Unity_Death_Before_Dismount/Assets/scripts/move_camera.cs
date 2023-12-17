using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour
{
    #region Variables
    [Header("Assets")]
    public GameObject GameManager;
    public GameObject tank;
    #endregion

    #region Builtin Methods
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().finalApproach)
        {
            transform.position = new Vector3(0, 5, tank.transform.position.z - 12);
        }
    }
    #endregion
}
