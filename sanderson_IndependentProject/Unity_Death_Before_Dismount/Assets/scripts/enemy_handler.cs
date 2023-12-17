using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_handler : MonoBehaviour
{
    #region Variables
    [Header("Game Manager")]
    public GameObject GameManager;

    [Header("Assets")]
    public GameObject tank;
    public GameObject parent;
    public Animator animator;
    public GameObject backBlast;

    [Header("Stats")]
    public bool isDead = false;
    public bool fromLeft = true;
    public bool ambush = false;
    public float ambushDist;

    [Header("Sound Effects")]
    public AudioSource scream;
    public AudioSource rocket_hit;
    public AudioSource rocket_miss;
    #endregion

    #region Builtin Methods
    void Start()
    {
        ambushDist = (Random.Range(24, 46));
    }

    void Update()
    {
        if (!ambush)
        {
            if (Vector3.Distance(transform.position, tank.transform.position) <= ambushDist)
            {
                ambush = true;
                if (fromLeft)
                {
                    enterLeft();
                }
                else
                {
                    enterRight();
                }
            }
        }
    }
    #endregion

    #region Custom Methods
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isDead)
            {
                killEnemy();
            }
        }
    }

    public void killEnemy()
    {
        if (!isDead)
        {
            animator.SetBool("isDead", true);
            scream.Play();
            isDead = true;
            Destroy(parent, 3);
        }
    }

    public void aimRocket()
    {
        if (fromLeft)
        {
            transform.Rotate(0f, transform.rotation.y - Vector3.Angle(transform.forward, tank.transform.position - transform.position), 0f);
        }
        else
        {
            transform.Rotate(0f, transform.rotation.y + Vector3.Angle(transform.forward, tank.transform.position - transform.position), 0f);
        }
    }


    public void standTurn()
    {
        if (fromLeft)
        {
            Vector3 newRotation = new Vector3(0, 270, 0);
            transform.eulerAngles = newRotation;
        }
        else
        {
            Vector3 newRotation = new Vector3(0, 90, 0);
            transform.eulerAngles = newRotation;
        }
        animator.SetBool("runAway", true);
    }

    public void fireRocket()
    {
        if (!GameManager.GetComponent<GameManager>().smokeCover)
        {
            backBlast.SetActive(true);
            if (Random.Range(0, 3) == 0)
            {
                rocket_hit.Play();

                if(fromLeft)
                {
                    Invoke("damageLeft", 3.4f);
                }
                else
                {
                    Invoke("damageRight", 3.4f);
                }
            }
            else
            {
                rocket_miss.Play();
            }
        }
    }

    private void damageLeft()
    {
        if (tank.GetComponent<playerTank>().damageLeft.activeSelf)
        {
            tank.GetComponent<playerTank>().damageLeft.SetActive(false);
        }

        tank.GetComponent<playerTank>().damageLeft.SetActive(true);
        GameManager.GetComponent<GameManager>().takeDamage(0.0f, 20.0f);
    }

    private void damageRight()
    {
        if (tank.GetComponent<playerTank>().damageRight.activeSelf)
        {
            tank.GetComponent<playerTank>().damageRight.SetActive(false);
        }
        tank.GetComponent<playerTank>().damageRight.SetActive(true);
        GameManager.GetComponent<GameManager>().takeDamage(0.0f, 20.0f);
    }

    public void fadeAway()
    {
        Destroy(parent);
    }

    private void enterLeft()
    {
        float num = (Random.Range(26, 61) / 10f);
        Invoke("nowCrouch", num);
        animator.SetBool("enterLeft", true);
    }

    private void enterRight()
    {
        float num = (Random.Range(26, 61) / 10f);
        Invoke("nowCrouch", num);
        animator.SetBool("enterRight", true);
    }

    private void nowCrouch()
    {
        animator.SetBool("nowCrouch", true);
    }
#endregion
}
