using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Light light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) { GetComponent<Light>().enabled = !GetComponent<Light>().enabled;}
    }
}
