using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabStart : MonoBehaviour
{

    public GameObject prefab;
    private float x;
    private Vector3 lampPosition;
    // Start is called before the first frame update
    void Start()
    {

        for (float i = 0; i < 10; i++)
        {
            x = transform.position.z + (i * 1);
            lampPosition = new Vector3(5, 0, x);
            Instantiate(prefab, lampPosition, transform.rotation);
        }

    }

    // Update is called once per frame
    void Update()
    {


    }
}
