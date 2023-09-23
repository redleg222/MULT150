using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBirthday : MonoBehaviour
{
    public int birthMonth;
    public int birthDay;

    // Start is called before the first frame update
    void Start()
    {
        if (birthMonth >= 1 && birthMonth <= 12 && birthDay >= 1 && birthDay <= 31)
        {
            for (int m = 1; m <= 12; m++)
            {
                if (m == birthMonth)
                {
                    Debug.Log("It's my birth month!");
                    int maxdays = System.DateTime.DaysInMonth(2020, m); //assuming leapyear
                    for (int d = 1; d <= maxdays; d++)
                    {
                        if (d == birthDay)
                        {
                            Debug.Log("It's my birthday!");
                        }
                        else
                        {
                            Debug.Log("Day: " + d.ToString());
                        }
                    }

                }
                else
                {
                    Debug.Log("Month: " + m.ToString());
                }
            }
        } else
        {
            Debug.Log("Error: declare proper variables");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
