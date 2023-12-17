using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Canvas")]
    public Canvas UI;

    [Header("Assets")]
    public GameObject tank;
    public GameObject road;
    public GameObject finishLine;

    [Header("Status")]
    public bool gameStart = false;
    public int houseCountdown = 100;
    public float roadSpeed;
    public bool smokeCover = false;
    public bool finalApproach = false;

    [Header("Stats")]
    public float fuel = 100.0f;
    public float health = 100.0f;
    public float ammo = 100.0f;
    public int smoke = 3;
    public GameObject smoke01;
    public GameObject smoke02;
    public GameObject smoke03;

    [Header("Obstacles")]
    public GameObject obstacleTank;
    public GameObject obstacleWire;

    [Header("PowerUps")]
    public GameObject powerupFuel;
    public GameObject powerupHealth;
    public GameObject powerupAmmo;

    [Header("Prefabs")]
    public GameObject houses;
    public GameObject smokeScreen;
    public GameObject enemyLeft;
    public GameObject enemyRight;

    [Header("Progress Bars")]
    public GameObject pbFuel;
    public GameObject pbHealth;
    public GameObject pbAmmo;
    public GameObject pbHouses;

    [Header("Sound Effects")]
    public AudioSource backgroundMusic;
    public AudioSource shootMG;
    public AudioSource endGame;

    private bool spawnedObstacles = false;
    private int powerUp = 100;
    private int startingHouses;

    private Renderer rend;
    private float dmgPerframe = 0;
    #endregion

    #region Builtin Methods
    void Start()
    {
        rend = road.GetComponent<Renderer>();
        startingHouses = houseCountdown;
    }

    void Update()
    {

        UpdateStats();
        FireMG();

        if (houseCountdown == 0)
        {
            prepFinish();
        }
        else if (houseCountdown != 0)
        {
            MoveAssets();

            if (GameObject.FindGameObjectsWithTag("house").Length < 12)
            {
                Instantiate(houses, houses.transform.position, houses.transform.rotation);
                lowerHousecount();
                setAmbushes();

                if (spawnedObstacles)
                {
                    spawnedObstacles = !spawnedObstacles;
                    if (houseCountdown <= powerUp)
                    {
                        powerUp = powerUp - Random.Range(7, 15);
                        createPowerups();
                    }
                }
                else
                {
                    float fObstacles, pos, newpos;

                    spawnedObstacles = !spawnedObstacles;

                    fObstacles = Random.Range(0, 3);
                    pos = -1;

                    for (int i = 0; i < fObstacles; i++)
                    {
                        newpos = Random.Range(-1, 2) * 6;
                        while (pos == newpos)
                        {
                            newpos = Random.Range(-1, 2) * 6;
                        }
                        pos = newpos;
                        createObstacles(pos);
                    }
                }
            }
        }
    }
    #endregion

    #region Custom Methods
    public RaycastHit hit;
    private void UpdateStats()
    {

        health -= dmgPerframe;
        if (health < 0)
        {
            health = 0;
        }
        pbFuel.GetComponent<ProgressBar>().UpdateValue((int)fuel);
        pbHealth.GetComponent<ProgressBar>().UpdateValue((int)health);
        pbAmmo.GetComponent<ProgressBar>().UpdateValue((int)ammo);
        pbHouses.GetComponent<ProgressBar>().UpdateValue(startingHouses - (startingHouses - houseCountdown));

        if (health <= 0)
        {
            GameOver(true);
        }

        float speed = tank.GetComponent<playerTank>().currentSpeed;

        //consume fuel
        if (speed != 0)
        {
            float rate = 600.0f;
            if (tank.GetComponent<playerTank>().highGear)
            {
                rate *= .9f;
            }
            fuel -= (speed / rate);
        }
        else if (gameStart)
        {
            fuel -= 0.001f;
        }

        if (fuel <= 0)
        {
            fuel = 0;
            GameOver();
        }
    }

    private void GameOver(bool explosion = false)
    {
        UI.GetComponent<trigger_canvas>().gameLost.SetActive(true);
        UI.GetComponent<trigger_canvas>().buttonReplay.SetActive(true);
        endGame.enabled = true;
        if (explosion)
        {
            tank.GetComponent<playerTank>().destroyedTank.SetActive(true);
        }
        tank.GetComponent<playerTank>().tankOperational = false;
    }

    private void FireMG()
    {
        if (tank.GetComponent<playerTank>().tankOperational)
        {
            if (!Input.GetMouseButton(0) || ammo <= 0)
            {
                shootMG.mute = true;
                if (ammo <= 0.0f) { ammo = 0.0f; }
            }
            else if (Input.GetMouseButton(0)) //GetMouseButtonDown
            {
                shootMG.mute = false;
                ammo -= 0.05f;

                Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayOrigin, out hit))
                {
                    if (hit.collider.tag == "enemy")
                    {
                        //Debug.DrawRay(Camera.main.transform.forward, hit.point, Color.green, 10);
                        hit.collider.GetComponent<enemy_handler>().killEnemy();
                    }
                }
            }
        }
    }

    public void popSmoke()
    {
        if (tank.GetComponent<playerTank>().tankOperational)
        {
            if (!smokeCover)
            {
                if (smoke == 3)
                {
                    smoke01.transform.GetChild(0).gameObject.SetActive(false);
                }
                else if (smoke == 2)
                {
                    smoke02.transform.GetChild(0).gameObject.SetActive(false);
                }
                else if (smoke == 1)
                {
                    smoke03.transform.GetChild(0).gameObject.SetActive(false);
                }
                smoke -= 1;
                smokeCover = true;
                GameObject temp = Instantiate(smokeScreen) as GameObject;
                Vector3 position = tank.transform.position;
                position.z += 8;
                temp.transform.position = position;
            }
        }
    }

    public void lowerHousecount()
    {
        if (houseCountdown > 0)
        {
            houseCountdown -= 1;
        }
    }

    private void MoveAssets()
    {
        float offset = Time.time * (roadSpeed / -6);
        rend.material.mainTextureOffset = new Vector2(0, offset);
        advanceAssets("house", roadSpeed);
        advanceAssets("powerup", roadSpeed);
        advanceAssets("obstacle", roadSpeed);
        advanceAssets("enemy", roadSpeed);
    }

    public void advanceAssets(string tag, float spd)
    {
        GameObject[] collection = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject ob in collection)
        {
            ob.transform.position += Vector3.forward * spd * -2 * Time.deltaTime;
        }
    }

    public void createObstacles(float xOffset = 0.0f) //1 = tank_obstacle 2=wire_obstacle
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                createObstacle(obstacleTank, xOffset, 2);
                break;
            case 1:
                createObstacle(obstacleWire, xOffset, 5);
                break;
        }
    }

    public void createObstacle(GameObject obs, float xOffset, int iChild)
    {
        GameObject temp, child;
        temp = Instantiate(obs) as GameObject;
        child = temp.transform.GetChild(iChild).GetComponent<trigger_obstacles>().GameManager = this.gameObject;
        Vector3 position = temp.transform.position;
        position.x = xOffset;
        temp.transform.position = position;
    }

    public void hitObstacle(int i)
    {
        if (i == 1)
        {
            takeDamage(0.0f, 5.0f);
        }
        else if (i == 2)
        {
            tank.GetComponent<playerTank>().HalfSpeed = !tank.GetComponent<playerTank>().HalfSpeed;
            if (tank.GetComponent<playerTank>().HalfSpeed)
            {
                takeDamage(0.005f, 0.0f);
            }
            else
            {
                takeDamage(0.0f, 0.0f);
            }
        }
    }

    public void setAmbushes()
    {

        switch (Random.Range(0, 12))
        {
            case 0:
                setAmbush(enemyLeft);
                setAmbush(enemyRight);
                break;
            case 1:
                setAmbush(enemyLeft);
                break;
            case 2:
                setAmbush(enemyLeft);
                break;
            case 3:
                setAmbush(enemyRight);
                break;
            case 4:
                setAmbush(enemyRight);
                break;
            default:
                break;
        }
    }

    public void setAmbush(GameObject enemy)
    {
        GameObject temp;
        temp = Instantiate(enemy) as GameObject;
        temp.transform.GetComponent<enemy_handler>().GameManager = this.gameObject;
        temp.transform.GetComponent<enemy_handler>().tank = tank;
    }

    public void takeDamage(float perFrame = 0, float flatDamage = 0)
    {
        dmgPerframe = perFrame;
        if (flatDamage != 0)
        {
            if (flatDamage > health)
            {
                health = 0;
            }
            else
            {
                health -= flatDamage;
            }
            flatDamage = 0;
            pbHealth.GetComponent<ProgressBar>().UpdateValue((int)health);

            if (health <= 0)
            {
                GameOver(true);
            }
        }
    }

    public void createPowerups() //1 = fuel 2=health 3=ammo
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                createPowerup(powerupFuel);
                break;
            case 1:
                createPowerup(powerupHealth);
                break;
            case 2:
                createPowerup(powerupAmmo);
                break;
        }
    }

    public void createPowerup(GameObject pu)
    {
        GameObject temp = Instantiate(pu) as GameObject;
        temp.GetComponent<trigger_powerup>().GameManager = gameObject;
        Vector3 position = temp.transform.position;
        position.x = Random.Range(-1, 2) * 6;
        temp.transform.position = position;
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

    public void prepFinish()
    {
        pbHouses.SetActive(false);
        finalApproach = true;

        finishLine.SetActive(true);

        GameObject[] fogs;
        fogs = GameObject.FindGameObjectsWithTag("FoW");
        if (fogs.Length > 0)
        {
            foreach (GameObject fog in fogs)
            {
                Destroy(fog);
            }
        }

    }
    #endregion
}