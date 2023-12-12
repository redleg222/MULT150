using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour
{
    [SerializeField] private GameObject tank, road, trigger;
    [SerializeField] public int houseCountdown = 100, smoke = 3;
    [SerializeField] public float health = 100.0f, fuel = 100.0f, ammo = 100.0f;

    private Renderer rend;
    private Rigidbody rigTank;
    private shermanTank code;
    private float dmgPerframe = 0;
    [SerializeField] public GameObject pbFuel, pbHealth, pbAmmo;

    private GameObject[] houses, powerups, enemies, obstacles;

    public void lowerHousecount()
    {
        if (houseCountdown > 0)
        {
            houseCountdown -= 1;
        }
    }

    public void winZone()
    {
        Debug.Log("Chicken Dinner");
    }

    public void takeDamage(float perFrame = 0, float flatDamage = 0)
    {
        dmgPerframe = perFrame;
        health -= flatDamage;
    }

    public int houseCount()
    {
        return houseCountdown;
    }

    public void powerup(int i)
    {
        if (i == 1)
        {
            if (fuel >= 70.0f) 
            {
                fuel = 100.0f;
            }
            else
            {
                fuel += 30.0f;
            }
        }
        else if (i == 2)
        {
            if (health >= 70.0f)
            {
                health = 100.0f;
            }
            else
            {
                health += 30.0f;
            }
        }
        else if (i == 3)
        {
            if (ammo >= 70.0f)
            {
                ammo = 100.0f;
            }
            else
            {
                ammo += 30.0f;
            }
        }
    }

    void Start()
    {
        rend = road.GetComponent<Renderer>();
        rigTank = tank.GetComponent<Rigidbody>();
        code = tank.GetComponent<shermanTank>();
        pbFuel.GetComponent<ProgressBar>().UpdateValue((int)fuel);
        pbHealth.GetComponent<ProgressBar>().UpdateValue((int)health);
        pbAmmo.GetComponent<ProgressBar>().UpdateValue(ammo);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ammo <= 0)
        {
            ammo = 0.0f;
            //code.shoot.Pause();
        }
        else
        {
            //code.shoot.Play();
            if (Input.GetMouseButton(0)) //GetMouseButtonDown
            {
                ammo -= 0.1f;

                if (Physics.Raycast(rayOrigin,out hit))
                {
                    if (hit.collider.tag == "enemy")
                    {
                        Debug.Log("Die!");
                    }
                }
            }
        }

        health -= dmgPerframe;
        pbFuel.GetComponent<ProgressBar>().UpdateValue((int)fuel);
        pbHealth.GetComponent<ProgressBar>().UpdateValue((int)health);
        pbAmmo.GetComponent<ProgressBar>().UpdateValue((int)ammo);

        float speed = tank.GetComponent<shermanTank>().fSpeed;
        if (speed != 0)
        {
            fuel -= (speed / 1000.0f);
        }
        else
        {
            fuel -= 0.001f;
        }
        
        if (houseCountdown != 0)
        {
            float offset = Time.time * speed / -6;
            rend.material.mainTextureOffset = new Vector2(0, offset);
            houses = GameObject.FindGameObjectsWithTag("house");
            powerups = GameObject.FindGameObjectsWithTag("powerup");
            obstacles = GameObject.FindGameObjectsWithTag("obstacle");
            enemies = GameObject.FindGameObjectsWithTag("enemy");

            foreach (GameObject house in houses)
            {
                house.transform.position += Vector3.forward * speed * -2 * Time.deltaTime;
            }

            foreach (GameObject powerup in powerups)
            {
                powerup.transform.position += Vector3.forward * speed * -2 * Time.deltaTime;
            }

            foreach (GameObject obstacle in obstacles)
            {
                obstacle.transform.position += Vector3.forward * speed * -2 * Time.deltaTime;
            }

            foreach (GameObject enemy in enemies)
            {
                enemy.transform.position += Vector3.forward * speed * -2 * Time.deltaTime;
            }
        }
    }
}