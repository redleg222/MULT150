using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTank : MonoBehaviour
{
    #region Variables
    [Header("Game Manager")]
    public GameObject gameManager;

    [Header("Movement Properties")]
    public float maxSpeed = 2f;
    public float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }
    public bool highGear = false;
    public bool HighGear
    {
        get { return highGear; }
    }
    public bool halfSpeed = false;
    public float tankRotationSpeed = 20f;

    [Header("Turret Properties")]
    public Transform turretTransform;
    public float turretLagSpeed = 0.5f;

    [Header("Sound Effects")]
    public AudioSource idle;
    public AudioSource move;

    [Header("Damage Effects")]
    public GameObject damageLeft;
    public GameObject damageRight;
    public GameObject destroyedTank;

    private Rigidbody rb;
    private float MaxSpeed;
    private inputsPlayer input;
    private Vector3 finalTurretLookDir;
    private Renderer rendL, rendR;
    private float fDir;
    private float fLturn, fRturn;
    private bool bMove, bSpin;
    #endregion

    #region Properties
    public bool HalfSpeed
    {
        get { return halfSpeed; }
        set { halfSpeed = value; }
    }
    #endregion

    #region Builtin Methods
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<inputsPlayer>();
        rendL = this.transform.GetChild(3).gameObject.GetComponent<Renderer>();
        rendR = this.transform.GetChild(4).gameObject.GetComponent<Renderer>();
    }

    void FixedUpdate() 
    {
        if (rb && input) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                highGear = true;
                MaxSpeed = maxSpeed * 1.5f;
            }
            else
            {
                highGear = false;
                MaxSpeed = maxSpeed;
            }


            if (Input.GetKey(KeyCode.Q))
            {
                if (gameManager.GetComponent<game_manager>().Smoke > 0)
                {
                    gameManager.GetComponent<game_manager>().popSmoke();
                }
                
            }

            HandleMovement();
            HandleTurret();
            AnimateMovement();
            MovementAudio();
        }
    }
    #endregion

    #region Custom Methods
    protected virtual void HandleMovement()
    {
        if (input.ForwardInput == 0)
        {
            currentSpeed = 0f;
        }
        else if (input.ForwardInput < 0)
        {
            currentSpeed = (MaxSpeed * 0.3f);
        }
        else
        {
            currentSpeed = MaxSpeed;
        }

        if (halfSpeed)
        {
            currentSpeed *= 0.5f;
        }



        float dir = Mathf.Abs(Mathf.Round(this.transform.rotation.y * 180f));

        if (dir <= 69) //facing forward
        {
            gameManager.GetComponent<game_manager>().RoadSpeed = currentSpeed;
        }
        else if (dir >= 166) //facing rear
        {
            gameManager.GetComponent<game_manager>().RoadSpeed = (currentSpeed * -0.3f);
        }
        else if (input.ForwardInput < 0)
        {
            gameManager.GetComponent<game_manager>().RoadSpeed = -0.005f;
        }
        else
        {
            gameManager.GetComponent<game_manager>().RoadSpeed = 0.01f;
        }

        if (!gameManager.GetComponent<game_manager>().FinalApproach)
        {
            if (input.ForwardInput >= 0)
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
                if (wantedPosition.z > 2.4)
                {
                    wantedPosition.z = 2.4f;
                }
                rb.MovePosition(wantedPosition);
            }
            else
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
                if (wantedPosition.z < -1.2)
                {
                    wantedPosition.z = -1.2f;
                }
                rb.MovePosition(wantedPosition);
            }
        }
        else
        {
            if (input.ForwardInput >= 0)
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
                rb.MovePosition(wantedPosition);
                //gameManager.GetComponent<game_manager>().GetComponent<Camera>().transform.position = gameManager.GetComponent<game_manager>().GetComponent<Camera>().transform.position + new Vector3(0, 0, Vector3.Distance(transform.position, gameManager.GetComponent<game_manager>().GetComponent<Camera>()));
            }
            else
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
                if (wantedPosition.z < -1.2)
                {
                    wantedPosition.z = -1.2f;
                }
                rb.MovePosition(wantedPosition);
            }
        }
        

        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput  * Time.deltaTime));
        rb.MoveRotation(wantedRotation);
    }

    protected virtual void HandleTurret()
    {
        if (turretTransform)
        {
            Vector3 turretLookDir = input.RecticlePosition - turretTransform.position;
            turretLookDir.y = 0f;

            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Time.deltaTime * turretLagSpeed);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
        }
    }

    protected virtual void AnimateMovement()
    {
        float offset;
        if (input.ForwardInput != 0)
        {
            offset = Time.time * currentSpeed * input.ForwardInput;
        }
        else if (currentSpeed != 0)
        {
            offset = Time.time * currentSpeed * Mathf.Abs(input.RotationInput);
        }
        else 
        {
            offset = Time.time * MaxSpeed * Mathf.Abs(input.RotationInput);
        }

        rendL.material.SetTextureOffset("_MainTex", new Vector2((offset * input.TurnL), 0f));
        RotateWheels(this.transform.GetChild(6).gameObject, (offset * input.TurnL));

        rendR.material.SetTextureOffset("_MainTex", new Vector2((offset * input.TurnR), 0f));
        RotateWheels(this.transform.GetChild(7).gameObject, (offset * input.TurnR));
    }

    protected virtual void RotateWheels(GameObject wheels, float offset)
    {
        for (int i = 0; i < wheels.transform.childCount; i++)
        {
            float wheelSize = wheels.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = offset / wheelSize;
            wheels.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }
    }

    protected virtual void MovementAudio()
    {
        if (input.ForwardInput + input.RotationInput == 0)
        {
            idle.mute = false;
            move.mute = true;
        }
        else
        {
            idle.mute = true;
            move.mute = false;
        }
    }

    #endregion
}
