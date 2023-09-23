using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evens : MonoBehaviour
{
    public int start; //set variable to 22
    public int end; //set variable to 100

    // Start is called before the first frame update
    void Start()
    {
        if (start == 0)
        {
            Debug.Log("Variable 'start' set to 22");
            start = 22;
                }
        if (end == 0)
        {
            Debug.Log("Variable 'end' set to 100");
            end = 100;
                }
        int sum = start;
        while (sum <= end)
        {
            Debug.Log(sum);
            sum += 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}