using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shermanTank : MonoBehaviour
{
    [SerializeField] public float fSpeed = 0.0f, maxSpeed = 1.0f, revSpeed = 0.2f, rotationSpeed = 2.0f;//Replace with your max speed
    [SerializeField] private GameObject turret, aim_point;
    [SerializeField] private GameObject Ltrack, Rtrack;
    [SerializeField] private GameObject Lwheel, Rwheel;
    [SerializeField] public bool canMove = true, halfSpeed = false;
    [SerializeField] private GameObject gameManager;

    private float rotationInput, forwardInput;
    public float offset;

    private Rigidbody rb;
    private Renderer rendL, rendR;
    public float fDir;
    private float fLturn, fRturn;
    private bool bMove, bSpin;

    public void obstacle(int i)
    {
        if (i == 1)
        {
            canMove = false;
            gameManager.GetComponent<game_manager>().takeDamage(0.0f,5.0f);

        }
        else if (i == 2)
        {
            halfSpeed = !halfSpeed;
            if (halfSpeed)
            {
                gameManager.GetComponent<game_manager>().takeDamage(0.005f, 0.0f);
            }
            else
            {
                gameManager.GetComponent<game_manager>().takeDamage(0.0f, 0.0f);
            }
        }
    }

    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody>();
        rendL = Ltrack.GetComponent<Renderer>();
        rendR = Rtrack.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(canMove);
            obstacle(1);
            Debug.Log(canMove);
        }

        GetInput();
 

        Vector3 newPosition = transform.position + (transform.forward * forwardInput * maxSpeed * Time.deltaTime);
        if (newPosition.z >= 4.0f)
        {
            newPosition.z = 4.0f;
        }
        rb.MovePosition(newPosition);

        Quaternion newRotation = transform.rotation * Quaternion.Euler(Vector3.up * (rotationInput * rotationSpeed * Time.deltaTime));
        rb.MoveRotation(newRotation);
 
        //Debug.Log("Y:" + Input.mousePosition.x);
        // Vector3 turDirection = Input.mousePosition - turret.position;
        // Quaternion rotation = Quaternion.LookRotation(direction);
        // transform.rotation = rotation;

        if (!bMove && bSpin)
        {
            offset = Time.time * maxSpeed;
            rendL.material.SetTextureOffset("_MainTex", new Vector2(offset * fLturn, 0f));
            rendR.material.SetTextureOffset("_MainTex", new Vector2(offset * fRturn, 0f));

            for (int i = 0; i < Lwheel.transform.childCount; i++)
            {
                //Calculate rotation based on wheel diameter and speed value
                float wheelSize = Lwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
                float rotationAngle = (offset * fLturn) / wheelSize;

                //Apply rotation
                Lwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
            }

            for (int i = 0; i < Rwheel.transform.childCount; i++)
            {
                //Calculate rotation based on wheel diameter and speed value
                float wheelSize = Rwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
                float rotationAngle = (offset * fRturn) / wheelSize;

                //Apply rotation
                Rwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
            }
        }
        else 
        { 
            float offset = Time.time * maxSpeed * fDir;
            rendL.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
            rendR.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));

            for (int i = 0; i < Lwheel.transform.childCount; i++)
            {
             //Calculate rotation based on wheel diameter and speed value
               float wheelSize = Lwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
               float rotationAngle = offset / wheelSize;

            //Apply rotation
               Lwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
            }

            for (int i = 0; i < Rwheel.transform.childCount; i++)
            {
             //Calculate rotation based on wheel diameter and speed value
               float wheelSize = Rwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
               float rotationAngle = offset / wheelSize;

            //Apply rotation
               Rwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
            }
        }
    }

    private void GetInput()
    {

        rotationInput = Input.GetAxis("Horizontal");

        if (rotationInput != 0) 
        {
            fLturn = rotationInput;
            fRturn = rotationInput * -1;
            bSpin = true;
        } 
        else
        {
            fLturn = 1.0f;
            fRturn = 1.0f;
            bSpin = false;
        }

        if (canMove)
        {
            forwardInput = Input.GetAxis("Vertical");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            forwardInput = -1;
            canMove = true;
        }
        else
        {
            forwardInput = 0;
        }

        if (forwardInput > 0)
        {
            fDir = 1.0f;
            fSpeed = fDir * maxSpeed;
            bMove = true;
        }
        else if (forwardInput < 0)
        {
            fDir = -1.0f;
            fSpeed = fDir * revSpeed;
            bMove = true;
        }
        else
        {
            fDir = 0.0f;
            fSpeed = fDir * maxSpeed;
            bMove = false;
        }

        if (halfSpeed)
        {
            fSpeed = fSpeed * 0.33f;
        }


    }




}