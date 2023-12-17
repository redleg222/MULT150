using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputsPlayer : MonoBehaviour
{
    #region Variables
    [Header("Input Properties")]
    new public Camera camera;
    #endregion

    #region Properties
    private Vector3 recticlePosition;
    public Vector3 RecticlePosition
    {
        get { return recticlePosition; }
    }

    private Vector3 recticleNormal;
    public Vector3 RecticleNormal
    {
        get { return recticleNormal; }
    }

    private float forwardInput;
    public float ForwardInput
    {
        get { return forwardInput; }
    }

    private float rotationInput;
    public float RotationInput
    {
        get { return rotationInput; }
    }

    private float turnL;
    public float TurnL
    {
        get { return turnL; }
    }

    private float turnR;
    public float TurnR
    {
        get { return turnR; }
    }

    #endregion

    #region Builtin Methods
    void Update()
    {
        if (camera)
        {
            HandleInputs();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(recticlePosition, 0.5f);
    }

    #endregion

    #region Custom Methods
    protected virtual void HandleInputs()
    {
        Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit))
        {
            recticlePosition = hit.point;
            recticleNormal = hit.normal;
        }

        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        if (rotationInput == 0)
        {
            turnL = 1f;
            turnR = 1f;
        }
        else if (rotationInput < 0)
        {
            turnL = rotationInput;
            turnR = rotationInput * -1;
        }
        else if (rotationInput > 0)
        {
            turnL = rotationInput * -1;
            turnR = rotationInput;
        }
    }
    #endregion
}
