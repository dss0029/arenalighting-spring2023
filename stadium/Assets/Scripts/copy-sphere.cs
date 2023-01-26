using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates 10 copies of Prefab each 2 units apart from each other

public class Example : MonoBehaviour
{
    public GameObject Sphere;
    void Start()
    {
        for (var i = 0; i < 10; i++)
        {
            Instantiate(Sphere, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        }
    }
}