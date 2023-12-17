using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_win : MonoBehaviour
{
    #region Variables
    [Header("Canvas")]
    public Canvas UI;

    [Header("Game Manager")]
    public GameObject GameManager;

    [Header("Assets")]
    public GameObject tank;

    [Header("Sound Effects")]
    public AudioSource winMusic;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.GetComponent<trigger_canvas>().gameWon.SetActive(true);
            UI.GetComponent<trigger_canvas>().buttonReplay.SetActive(true);
            tank.GetComponent<playerTank>().maxSpeed = 0f;
            winMusic.Play();
        }
    }
}
