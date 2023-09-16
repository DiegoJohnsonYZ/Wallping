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
        if (Input.GetKey(KeyCode.RightArrow) && !MurosManager.instance.IsHolding)
        {
            transform.position += new Vector3(0.02f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !MurosManager.instance.IsHolding)
        {
            transform.position += new Vector3(-0.02f, 0, 0);
        }

        if (Input.GetKeyDown("space"))
        {
            print("space");
            //holdTimer += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && !MurosManager.instance.IsWallping)
        {
            holdTimer += Time.deltaTime;
            MurosManager.instance.IsHolding = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(MoveToPosition(Camera.main.gameObject, new Vector3(Camera.main.transform.position.x, -1f, Camera.main.transform.position.z),0.2f));
            StartCoroutine(MoveToProjection(4, 0.2f));
            MurosManager.instance.IsWallping = true;
            MurosManager.instance.IsHolding = false;
            //print(holdTimer);
            
            GetComponent<SpriteRenderer>().color = Color.blue;
            Invoke("StopWallping", holdTimer*0.4f);
            holdTimer = 0;

        }

        if (MurosManager.instance.IsWallping)
        {

        }
        
    }

    void StopWallping()
    {
        StartCoroutine(MoveToPosition(Camera.main.gameObject,new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z),0.2f));
        StartCoroutine(MoveToProjection(5, 0.2f));
        print("stop wallping");
        MurosManager.instance.IsWallping = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!MurosManager.instance.IsWallping)
            print("gameover");

    }

    private IEnumerator MoveToPosition(GameObject objectToMove, Vector3 newPosition, float waitTime) {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, newPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        objectToMove.transform.position = newPosition;
        yield return null;
    }

    private IEnumerator MoveToScale(GameObject objectToScale, Vector3 newScale, float waitTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            objectToScale.transform.localScale = Vector3.Lerp(objectToScale.transform.position, newScale, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        objectToScale.transform.position = newScale;
        yield return null;
    }

    private IEnumerator MoveToProjection(float newSize, float waitTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newSize, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        Camera.main.orthographicSize = newSize;
        yield return null;
    }

}
