using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -0.02f, 0);
    }
}
