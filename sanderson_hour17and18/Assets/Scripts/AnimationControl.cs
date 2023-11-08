using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator> ();
        Debug.Log("Text" + anim.name);
    }

    void Update()
    {
        anim.SetFloat ("Speed", Input.GetAxis("Vertical"));
        anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
    }
}
