using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_handler : MonoBehaviour
{
    [SerializeField] public GameObject gameManager, tank;
    [SerializeField] public bool isDead = false;
    [SerializeField] public bool fromLeft = true;
    [SerializeField] public GameObject parent;
    [SerializeField] public AudioSource scream;
    [SerializeField] public AudioSource rocket_hit;
    [SerializeField] public AudioSource rocket_miss;
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject backBlast;
    [SerializeField] public float ambushDist;
    [SerializeField] public bool ambush = false;

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
        if (!gameManager.GetComponent<game_manager>().SmokeCover)
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
                Debug.Log("Rocket_Miss");
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
        gameManager.GetComponent<game_manager>().takeDamage(0.0f, 20.0f);
    }

    private void damageRight()
    {
        if (tank.GetComponent<playerTank>().damageRight.activeSelf)
        {
            tank.GetComponent<playerTank>().damageRight.SetActive(false);
        }
        tank.GetComponent<playerTank>().damageRight.SetActive(true);
        gameManager.GetComponent<game_manager>().takeDamage(0.0f, 20.0f);
    }

    public void fadeAway()
    {
        Destroy(parent);
    }

    private void enterLeft()
    {
        float num = (Random.Range(20, 61) / 10f);
        Invoke("nowCrouch", num);
        animator.SetBool("enterLeft", true);
    }

    private void enterRight()
    {
        float num = (Random.Range(20, 61) / 10f);
        Invoke("nowCrouch", num);
        animator.SetBool("enterRight", true);
    }

    private void nowCrouch()
    {
        animator.SetBool("nowCrouch", true);
    }

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
}
