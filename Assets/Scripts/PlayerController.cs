using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private float holdTimer;
    // Start is called before the first frame update
    void Start()
    {
        holdTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space");    
            //holdTimer += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            holdTimer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            print(holdTimer);
            holdTimer =0;
            
        }
        
    }
}
