using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour
{
    [SerializeField] public GameObject tank, road, finishLine;
    [SerializeField] public int houseCountdown = 100;
    [SerializeField] public float health = 100.0f, fuel = 100.0f, ammo = 100.0f;
    [SerializeField] public GameObject obstacleTank, obstacleWire;
    [SerializeField] public GameObject powerupFuel, powerupHealth, powerupAmmo;
    [SerializeField] public GameObject housesPrefab;
    [SerializeField] public GameObject smokeScreen;
    [SerializeField] private AudioSource shoot;
    [SerializeField] public Animator camAnimator;
    [SerializeField] public GameObject enemyLeft, enemyRight;

    private bool spawnedObstacles = false;
    private int powerUp = 100;

    private Renderer rend;
    private float dmgPerframe = 0;
    [SerializeField] public GameObject pbFuel, pbHealth, pbAmmo;

    #region Properties
    public int HouseCountdown
    {
        get { return houseCountdown; }
        set { houseCountdown = value; }
    }

    public int smoke = 3;
    public int Smoke
    {
        get { return smoke; }
        set { smoke = value; }
    }


    public bool smokeCover = false;
    public bool SmokeCover
    {
        get { return smokeCover; }
        set { smokeCover = value; }
    }

    public float roadSpeed;
    public float RoadSpeed
    {
        get { return roadSpeed; }
        set { roadSpeed = value; }
    }

    public bool finalApproach = false;
    public bool FinalApproach
    {
        get { return finalApproach; }
        set { finalApproach = value; }
    }
    #endregion

    #region Builtin Methods
    void Start()
    {
        rend = road.GetComponent<Renderer>();
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
                Instantiate(housesPrefab, housesPrefab.transform.position, housesPrefab.transform.rotation);
                lowerHousecount();
                setAmbushes();

                if (spawnedObstacles)
                {
                    spawnedObstacles = !spawnedObstacles;
                    if (HouseCountdown <= powerUp)
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

        if (health <= 0)
        {
            GameOver();
        }

        float speed = tank.GetComponent<playerTank>().CurrentSpeed;

        //consume fuel
        if (speed != 0)
        {
            float rate = 600.0f;
            if (tank.GetComponent<playerTank>().HighGear)
            {
                rate *= .9f;
            }
            fuel -= (speed / rate);
        }
        else
        {
            fuel -= 0.001f;
        }
    }



    private void GameOver()
    {
        tank.GetComponent<playerTank>().destroyedTank.SetActive(true);
    }

        private void FireMG()
    {
        if (!Input.GetMouseButton(0) || ammo <= 0)
        {
            shoot.mute = true;
            if (ammo <= 0.0f) { ammo = 0.0f; }
        }
        else if (Input.GetMouseButton(0)) //GetMouseButtonDown
        {
            shoot.mute = false;
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

    public void popSmoke()
    {
        smoke -= 1;
        smokeCover = true;
        GameObject temp = Instantiate(smokeScreen) as GameObject;
        Vector3 position = tank.transform.position;
        position.z += 8;
        temp.transform.position = position;
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
            //}
                
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
        child = temp.transform.GetChild(iChild).GetComponent<trigger_obstacles>().gameManager = this.gameObject;
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
        temp.transform.GetComponent<enemy_handler>().gameManager = this.gameObject;
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
                GameOver();
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
        temp.GetComponent<trigger_powerup>().gameManager = gameObject;
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

    public void finish()
    {
        Debug.Log("Chicken Dinner");
    }

    #endregion
}