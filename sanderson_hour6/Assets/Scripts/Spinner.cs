using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour{
    [SerializeField] private Vector3 _rotation;
    private float f;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(0, 2);
        f = Random.Range(1.0f, 6.0f);
        if (i == 0)
        {
            f = f * -1;
        }
        _rotation.y = _rotation.y * f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
