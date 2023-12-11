using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_controller : MonoBehaviour
{
 
    public float speed = 0.05f;
    public GameObject tank;
    public GameObject Ltrack;
    public GameObject Lwheel;
    public GameObject Rtrack;
    public GameObject Rwheel;
    public bool moveForward = true;
    public bool moveBackward = true;
    private Renderer rend;
    private Renderer rendL;
    private Renderer rendR;
    public GameObject[] houses;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rendL = Ltrack.GetComponent<Renderer>();
        rendR = Rtrack.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed * -1;
        rend.material.mainTextureOffset = new Vector2(0, offset);
        rendL.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        rendR.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        houses = GameObject.FindGameObjectsWithTag("houses");
        
        foreach (GameObject house in houses)
        {
            house.transform.position += Vector3.forward * speed * -12 * Time.deltaTime;
        }
        
        for (int i = 0; i < Lwheel.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = Lwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = speed / wheelSize;

            //Apply rotation
            Lwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }
        
        for (int i = 0; i < Rwheel.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = Rwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = speed / wheelSize;

            //Apply rotation
            Rwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }




        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("FORWARD");
            tank.transform.position += Vector3.forward * speed * 10 * Time.deltaTime;
            //speed += 0.05f;
            //Debug.Log(speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("BACK");
            tank.transform.position += Vector3.back * speed * 10 * Time.deltaTime;
            //speed -= 0.05f;
            //Debug.Log(speed);

        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("LEFT");
            tank.transform.Rotate(Vector3.up * speed * 4 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("RIGHT");
            tank.transform.Rotate(-Vector3.up * speed * 4 * Time.deltaTime);
        }
    }
}
