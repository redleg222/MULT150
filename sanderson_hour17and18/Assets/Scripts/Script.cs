using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("boolW", true);
        } else
        {
            anim.SetBool("boolW", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("boolA", true);
        }
        else
        {
            anim.SetBool("boolA", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("boolS", true);
        }
        else
        {
            anim.SetBool("boolS", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("boolD", true);
        }
        else
        {
            anim.SetBool("boolD", false);
        }
    }
}

