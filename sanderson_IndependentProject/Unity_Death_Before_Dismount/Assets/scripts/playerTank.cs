using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTank : MonoBehaviour
{
    #region Variables
    [Header("Game Manager")]
    public GameObject GameManager;

    [Header("Movement Properties")]
    public float maxSpeed = 2f;
    public float currentSpeed;
    public bool highGear = false;
    public bool halfSpeed = false;
    public float tankRotationSpeed = 20f;

    [Header("Turret Properties")]
    public Transform turretTransform;
    public float turretLagSpeed = 0.5f;

    [Header("Sound Effects")]
    public AudioSource idle;
    public AudioSource move;

    [Header("Damage Effects")]
    public bool tankOperational = true;
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

    public float halt;
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
            if (!tankOperational)
            {
                maxSpeed = 0;
            }

            if (Input.GetKey(KeyCode.LeftShift) && !halfSpeed)
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
                if (GameManager.GetComponent<GameManager>().smoke > 0)
                {
                    GameManager.GetComponent<GameManager>().popSmoke();
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
            GameManager.GetComponent<GameManager>().roadSpeed = currentSpeed * halt;
        }
        else if (dir >= 166) //facing rear
        {
            GameManager.GetComponent<GameManager>().roadSpeed = (currentSpeed * -0.3f * halt);
        }
        else if (input.ForwardInput < 0)
        {
            GameManager.GetComponent<GameManager>().roadSpeed = -0.005f * halt;
        }
        else
        {
            GameManager.GetComponent<GameManager>().roadSpeed = 0.01f * halt;
        }

        if (!GameManager.GetComponent<GameManager>().finalApproach && tankOperational)
        {
            if (input.ForwardInput >= 0)
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
                if (wantedPosition.z > 2.4)
                {
                    halt = 1f;
                    wantedPosition.z = 2.4f;
                }
                else if(wantedPosition.z < -1.2)
                {
                    halt = 0f;
                    wantedPosition.z = -1.2f;
                }
                else
                {
                    halt = 1f;
                }
                rb.MovePosition(wantedPosition);
            }
            else
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
                if (wantedPosition.z > 2.4)
                {
                    halt = 1f;
                    wantedPosition.z = 2.4f;
                }
                else if (wantedPosition.z < -1.2)
                {
                    halt = 0f;
                    wantedPosition.z = -1.2f;
                }
                else
                {
                    halt = 1f;
                }
                rb.MovePosition(wantedPosition);
            }
        }
        else if (tankOperational)
        {
            if (input.ForwardInput >= 0)
            {
                Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * currentSpeed * Time.deltaTime);
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
        
        if (tankOperational)
        {
            Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput * Time.deltaTime));
            rb.MoveRotation(wantedRotation);
        }
    }

    protected virtual void HandleTurret()
    {
        if (turretTransform && tankOperational)
        {
            Vector3 turretLookDir = input.RecticlePosition - turretTransform.position;
            turretLookDir.y = 0f;

            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Time.deltaTime * turretLagSpeed);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
        }
    }

    protected virtual void AnimateMovement()
    {
        if (tankOperational)
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
        if (!tankOperational)
        {
            idle.mute = true;
            move.mute = true;
        }
        else if (input.ForwardInput + input.RotationInput == 0)
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
