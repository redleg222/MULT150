using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_speed : MonoBehaviour
{

    public float track_speed = 0.0f;
    public GameObject obj;

    private float offset = 0.0f;
    private Renderer r;

    void Start()
    {
        r = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = (offset + Time.deltaTime * (track_speed * -1)) % 1f;
        r.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = obj.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = track_speed / wheelSize;

            //Apply rotation
            obj.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }
    }
}
