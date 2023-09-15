using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muros : MonoBehaviour
{

    public static Muros instance;
    
    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -0.02f, 0);

    }
}
