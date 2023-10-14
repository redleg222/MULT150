using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public bool counter = false;
    private int i = 0;

    void OnTriggerEnter(Collider other)
    {   if (counter)
        {
            i++;
            Debug.Log(other.gameObject.name + " has colided with " + gameObject.name + " " + i + " times");
        }
        else
        {
            Debug.Log(other.gameObject.name + " has colided with " + gameObject.name);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
